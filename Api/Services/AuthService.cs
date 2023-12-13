using Api.Dtos;
using Api.Helpers;
using Api.Services.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _repo;
    private readonly JwtSettingsDto jwtSettings;
    private readonly int tokenExpireHours;

    public AuthService(
        IRepository<User> repo,
        ConfigService configService
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

        return TokenHelper.GenerateJwtToken(tokenExpireHours, jwtSettings, user);
    }
}

