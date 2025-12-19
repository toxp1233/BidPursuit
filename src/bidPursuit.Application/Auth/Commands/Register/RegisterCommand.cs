using bidPursuit.Application.Auth.Dtos;
using MediatR;

namespace bidPursuit.Application.Auth.Commands.Register;

public record RegisterCommand(RegisterDto RegistrationParameters) : IRequest<TokenDto>;