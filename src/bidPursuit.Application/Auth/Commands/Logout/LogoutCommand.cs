using MediatR;

namespace bidPursuit.Application.Auth.Commands.Logout;

public record LogoutCommand(Guid UserId) : IRequest;
