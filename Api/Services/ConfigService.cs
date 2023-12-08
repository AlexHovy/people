using Api.Dtos;

namespace Api.Services;

public class ConfigService
{
    private readonly IConfiguration _configuration;

    public ConfigService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("People");
    }

    public JwtSettingsDto GetJwtSettings()
    {
        return _configuration.GetSection("Jwt").Get<JwtSettingsDto>();
    }
}
