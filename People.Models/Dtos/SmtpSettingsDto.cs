namespace People.Models.Dtos;

public class SmtpSettingsDto
{
    public string SMTP { get; set; }
    public int Port { get; set; }
    public string FromAddress { get; set; }
    public string FromPassword { get; set; }
}