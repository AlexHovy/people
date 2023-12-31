using Api.Dtos;
using Api.Services;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly int tokenExpireHours;

    public AuthController(
        ConfigService configService,
        IAuthService authService
    )
    {
        _authService = authService;

        tokenExpireHours = configService.GetTokenExpireHours();
    }

    [HttpPost("Login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto body)
    {
        try
        {
            var token = await _authService.Authenticate(body.Username, body.Password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            var tokenDto = new TokenDto
            {
                Token = token,
                ExpiresAt = DateTime.Now.AddHours(tokenExpireHours)
            };
            return Ok(tokenDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
