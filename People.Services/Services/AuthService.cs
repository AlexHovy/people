using People.Models.Dtos;
using People.Core.Helpers;
using People.Services.Services.Interfaces;
using People.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace People.Services.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _repo;
    private readonly JwtSettingsDto jwtSettings;
    private readonly int tokenExpireHours;

    public AuthService(
        IRepository<User> repo,
        IConfigService configService
    )
    {
        _repo = repo;
        jwtSettings = configService.GetJwtSettings();
        tokenExpireHours = configService.GetTokenExpireHours();
    }

    public async Task<string> Authenticate(string username, string password)
    {
        var user = await _repo.Query.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || !HashHelper.VerifyPasswordHash(user.PasswordHash, password))
        {
            return null;
        }

        return TokenHelper.GenerateJwtToken(
            tokenExpireHours,
            jwtSettings.Key,
            jwtSettings.Issuer,
            jwtSettings.Audience,
            user.Username
        );
    }
}

