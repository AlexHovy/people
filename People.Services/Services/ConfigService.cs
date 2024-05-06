using Microsoft.Extensions.Configuration;
using People.Services.Services.Interfaces;
using People.Core.Constants;
using People.Models.Dtos;

namespace People.Services.Services;

public class ConfigService : IConfigService
{
    private readonly IConfiguration _config;

    public ConfigService(IConfiguration config)
    {
        _config = config;
    }

    public string GetConnectionString()
    {
        return _config.GetConnectionString(ConfigKeys.ConnectionStringPeople);
    }

    public int GetTokenExpireHours()
    {
        return _config.GetSection(ConfigKeys.TokenExpireHours).Get<int>();
    }

    public JwtSettingsDto GetJwtSettings()
    {
        return _config.GetSection(ConfigKeys.Jwt).Get<JwtSettingsDto>();
    }

    public SmtpSettingsDto GetSmtpSettings()
    {
        return _config.GetSection(ConfigKeys.SmtpSettings).Get<SmtpSettingsDto>();
    }
}
