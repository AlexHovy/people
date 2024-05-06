using People.Models.Entities;
using People.Models.Dtos;

namespace People.Services.Services.Interfaces;

public interface IConfigService
{
    string GetConnectionString();
    int GetTokenExpireHours();
    JwtSettingsDto GetJwtSettings();
    SmtpSettingsDto GetSmtpSettings();
}
