namespace bidPursuit.Application.Auth.Dtos;

public class TokenDto
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
