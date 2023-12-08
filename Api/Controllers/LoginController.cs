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
    public ActionResult<string> Login(string username, string password)
    {
        var token = _authService.Authenticate(username, password);

        if (string.IsNullOrEmpty(token))
            return Unauthorized();

        return Ok(token);
    }
}
