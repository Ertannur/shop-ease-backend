using System.Net;
using System.Net.Mail;
using System.Text;
using ETicaret.Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace ETicaret.Infastructure.Services;

public class EmailService(IConfiguration configuration) :  IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        MailMessage mail = new();
        mail.IsBodyHtml = true;
        mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = body;
        mail.From = new(configuration["Mail:Username"], "E-Ticaret Sitesi",Encoding.UTF8);
        SmtpClient smtp = new();
        smtp.Credentials = new NetworkCredential(configuration["Mail:Username"],configuration["Mail:Password"]);
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Host = configuration["Mail:Host"];
        await smtp.SendMailAsync(mail);
    }
}