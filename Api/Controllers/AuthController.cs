using Api.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public ActionResult<string> Login(LoginDto body)
    {
        var token = _authService.Authenticate(body.Username, body.Password);
        if (string.IsNullOrEmpty(token))
            return Unauthorized();

        var tokenDto = new TokenDto
        {
            Token = token
        };
        return Ok(tokenDto);
    }
}
