using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auth.Commands.Logout;

public class LogoutCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw new NotFoundException(nameof(User), request.UserId.ToString());
        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
