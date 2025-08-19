using System.Collections.ObjectModel;
using System.Data;
using System.Net;
using ETicaret.API.Extensions;
using ETicaret.Infastructure.Services.Storage.Azure;
using ETicaret.SignalR.Hubs;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.MSSqlServer;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor(); // Client'tan gelen request neticesinde oluşturulan http context nesnesine katmanlardan erişebilmemizi sağlar
builder.Services.AddServices(builder.Configuration);
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
        columnOptions: new  ColumnOptions
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

builder.Services.AddStorage<AzureStorage>();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
await app.ExecuteAsync(); 

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated !=null ? context.User.Identity?.Name : "Anonymous";
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
    await next(); ; 
});
app.MapControllers();
app.MapHub<CustomerHub>("/chatHub");
app.Run();