using Api.DbContexts;
using Api.Dtos;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AuthService
{
    private readonly IRepository<User> _repo;
    private readonly JwtSettingsDto jwtSettings;

    public AuthService(
        IRepository<User> repo,
        ConfigService configService
    )
    {
        _repo = repo;
        jwtSettings = configService.GetJwtSettings();
    }

    public async Task<string> Authenticate(string username, string password)
    {
        var user = await _repo.Query.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || !HashHelper.VerifyPasswordHash(user.PasswordHash, password))
        {
            return null;
        }

        return TokenHelper.GenerateJwtToken(jwtSettings, user);
    }
}

