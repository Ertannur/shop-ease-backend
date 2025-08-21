using System.Collections.ObjectModel;
using System.Data;
using System.Net;
using ETicaret.API.Extensions;
using ETicaret.Infastructure.Services.Storage.Azure;
using ETicaret.SignalR.Hubs;
using ETicaret.SignalR.HubServices;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Kestrel yapılandırması
builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

// HttpContext erişimi
builder.Services.AddHttpContextAccessor();

// Özel servis kayıtları
builder.Services.AddServices(builder.Configuration);

// SignalR servisleri
builder.Services.AddSignalR();
builder.Services.AddScoped<CustomerHubService>();

// Azure Storage
builder.Services.AddStorage<AzureStorage>();

// Serilog yapılandırması
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MSSqlServerSinkOptions()
        {
            AutoCreateSqlDatabase = false,
            AutoCreateSqlTable = false,
            TableName = "Logs"
        },
        columnOptions: new ColumnOptions
        {
            AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn("UserName", SqlDbType.NVarChar, allowNull: true),
                new SqlColumn("IpAdress", SqlDbType.NVarChar, allowNull: true)
            }
        })
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Global exception middleware
app.UseMiddleware<ExceptionMiddleware>();
await app.ExecuteAsync();

// Swagger
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

// CORS ve HTTPS
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

// Kimlik doğrulama
app.UseAuthentication();
app.UseAuthorization();

// Serilog için kullanıcı adı ve IP loglama
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null ? context.User.Identity?.Name : "Anonymous";
    var host = Dns.GetHostEntry(Dns.GetHostName());
    string ip = "";
    foreach (var ipAddress in host.AddressList)
    {
        if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        {
            ip = ipAddress.ToString();
        }
    }
    LogContext.PushProperty("UserName", username);
    LogContext.PushProperty("IpAdress", ip);
    await next();
});

// Controller ve SignalR Hub endpointleri
app.MapControllers();
app.MapHub<CustomerHub>("/chatHub");

app.Run();
