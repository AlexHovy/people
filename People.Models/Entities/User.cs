using People.Models.Entities;

namespace People.Models.Entities;

public class User : Base
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}