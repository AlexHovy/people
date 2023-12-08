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
        return _config.GetConnectionString("People");
    }

    public JwtSettingsDto GetJwtSettings()
    {
        return _config.GetSection("Jwt").Get<JwtSettingsDto>();
    }
}
