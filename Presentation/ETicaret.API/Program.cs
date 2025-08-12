using ETicaret.API.Extensions;
using ETicaret.Infastructure.Services.Storage.Azure;
using ETicaret.SignalR;
using ETicaret.SignalR.Hubs;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);
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

app.MapControllers();
app.MapHub<CustomerHub>("/chatHub");
app.Run();