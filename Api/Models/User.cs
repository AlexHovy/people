using Api.Models;

namespace Api.Models;

public class User : Base
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}