using System.Security.Claims;
using System.Text;
using ETicaret.API.Localizations;
using ETicaret.API.Validations;
using ETicaret.Application.Abstractions;
using ETicaret.Application.Abstractions.Hubs;
using ETicaret.Application.Abstractions.Storage;
using ETicaret.Application.CQRS.Commands.Auths;
using ETicaret.Application.CQRS.Commands.Users;
using ETicaret.Application.CQRS.Handlers.Auths;
using ETicaret.Application.CQRS.Queries.Products;
using ETicaret.Domain.Entities;
using ETicaret.Infastructure.Services;
using ETicaret.Infastructure.Services.Storage;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Services;
using ETicaret.SignalR.HubServices;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using TokenHandler = ETicaret.Infastructure.Services.Token.TokenHandler;

namespace ETicaret.API.Extensions;

public static class ServiceRegistiration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddOpenApi();
        // cors politikası ayarlaması gerçekleştirildi
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
        

        // MediatR ayarlaması
        services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(
            typeof(Program).Assembly,
            typeof(RegisterUserCommandHandler).Assembly));
        // DbContext ayarlaması yapıldı 
        services.AddDbContext<ETicaretDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentity<AppUser, AppRole>(opt => // identity db context ayarları yapıldı
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 7;
            })
            .AddEntityFrameworkStores<ETicaretDbContext>()
            .AddRoles<AppRole>()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
            .AddDefaultTokenProviders();
       //Fluent Validation Configurations
       services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
       services.AddScoped<IValidator<RegisterUserCommandRequest>, RegisterUserValidator>();
       services.AddScoped<IValidator<LoginUserCommandRequest>, LoginUserValidator>();
       services.AddScoped<IValidator<GetProductsQuery>, GetProductsValidator>();
       services.AddScoped<IValidator<UpdateUserCommandRequest>, UpdateUserValidator>();
       services.AddFluentValidationAutoValidation();
       services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
       
        // Dependecy injection ayarlamaları
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGenericService, GenericService>(); // özelleştirme yapılacak
        services.AddScoped<ITokenService, TokenHandler>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IEmailService, EmailService>();
        // Jwt Token Ayarlamaları
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                        expires != null ? expires > DateTime.UtcNow : false,
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                    NameClaimType = ClaimTypes.Name
                };
            });
        
        services.AddScoped<IStorageService, StorageService>();
        
        // SignalR Konfigürasyonu
        services.AddTransient<ICustomerHubService, CustomerHubService>();
        services.AddSignalR();
        return services;
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage, T>();
    }
}