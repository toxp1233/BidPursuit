using bidPursuit.Application.Auth.Dtos;
using MediatR;

namespace bidPursuit.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<TokenDto>;
