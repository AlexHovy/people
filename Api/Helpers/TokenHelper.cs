using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Dtos;
using Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helpers;

public static class TokenHelper
{
    public static string GenerateJwtToken(int tokenExpireHours, JwtSettingsDto jwtSettings, User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        };

        var token = new JwtSecurityToken(
            jwtSettings.Issuer,
            jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddHours(tokenExpireHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}