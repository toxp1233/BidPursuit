using AutoMapper;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Admins.Commands.UpdateUser;

public class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(User), request.Id.ToString());
        mapper.Map(request, user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
