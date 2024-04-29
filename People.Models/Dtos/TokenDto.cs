namespace People.Models.Dtos;

public class TokenDto
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}