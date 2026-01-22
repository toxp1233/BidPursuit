using MediatR;

namespace bidPursuit.Application.Admins.Commands.DeactivateUser;

public record DeactivateUserCommand(Guid UserId) : IRequest;
