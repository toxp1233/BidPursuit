using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Admins.Commands.DeactivateUser;

public class DeactivateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeactivateUserCommand>
{
    public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw new NotFoundException(nameof(User), request.UserId.ToString());
        user.IsActive = false;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
