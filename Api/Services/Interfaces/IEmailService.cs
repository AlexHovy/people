namespace Api.Services.Interfaces;

public interface IEmailService
{
    Task SendAsync(List<string> to, string subject, string body);
}
