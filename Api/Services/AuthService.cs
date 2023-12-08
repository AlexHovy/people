using Api.DbContexts;
using Api.Dtos;
using Api.Helpers;

namespace Api.Services;

public class AuthService
{
    private readonly PeopleContext _context;
    private readonly JwtSettingsDto jwtSettings;

    public AuthService(PeopleContext context, ConfigService configService)
    {
        _context = context;
        jwtSettings = configService.GetJwtSettings();
    }

    public string Authenticate(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);
        if (user == null || !HashHelper.VerifyPasswordHash(user.PasswordHash, password))
        {
            return null;
        }

        return TokenHelper.GenerateJwtToken(jwtSettings, user);
    }
}

