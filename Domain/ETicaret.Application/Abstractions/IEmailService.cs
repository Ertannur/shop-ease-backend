namespace ETicaret.Application.Abstractions;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}