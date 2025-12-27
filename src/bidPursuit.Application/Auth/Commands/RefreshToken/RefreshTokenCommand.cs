using bidPursuit.Application.Auth.Dtos;
using MediatR;

namespace bidPursuit.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<TokenDto>
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = default!;
}
