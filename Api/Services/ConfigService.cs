using Api.Constants;
using Api.Dtos;

namespace Api.Services;

public class ConfigService
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

    public JwtSettingsDto GetJwtSettings()
    {
        return _config.GetSection(ConfigKeys.Jwt).Get<JwtSettingsDto>();
    }

    public SmtpSettingsDto GetSmtpSettings()
    {
        return _config.GetSection(ConfigKeys.SmtpSettings).Get<SmtpSettingsDto>();
    }
}
