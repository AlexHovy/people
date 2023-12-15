using Api.Dtos;
using Api.Helpers;
using Api.Services.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace Api.Services;

public class EmailService : IEmailService
{
    private readonly SmtpSettingsDto smtpSettings;

    public EmailService(
        ConfigService configService
    )
    {
        smtpSettings = configService.GetSmtpSettings();
    }

    public async Task SendAsync(List<string> to, string subject, string body)
    {
        try
        {
            var smtp = new SmtpClient
            {
                Host = smtpSettings.SMTP,
                Port = smtpSettings.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            if (!string.IsNullOrWhiteSpace(smtpSettings.FromPassword))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(
                    smtpSettings.FromAddress,
                    smtpSettings.FromPassword
                );
            }

            using (var message = new MailMessage()
            {
                From = new MailAddress(smtpSettings.FromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                foreach (var toEmail in to)
                {
                    message.To.Add(new MailAddress(toEmail));
                }

                await smtp.SendMailAsync(message);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

