namespace Api.Dtos;

public class TokenDto
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}