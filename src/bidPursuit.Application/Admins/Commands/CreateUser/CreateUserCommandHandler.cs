using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Enums;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Admins.Commands.CreateUser;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordHelper passwordHelper,
    IMapper mapper
    ) : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.GetByNameAsync(request.UserForCreation.Name, cancellationToken) is not null)
            throw new AlreadyExistsException(nameof(User), request.UserForCreation.Name);

        if (await userRepository.GetByEmailAsync(request.UserForCreation.Email, cancellationToken) is not null)
            throw new AlreadyExistsException(nameof(User), request.UserForCreation.Email);

        var newUser = mapper.Map<User>(request.UserForCreation);

        newUser.PasswordHash = passwordHelper.Hash(request.UserForCreation.Password);
        newUser.Role = request.Roles;

        if(request.Roles == Roles.Admin)
        {
            newUser.IsAdmin = true;
        }
        await userRepository.AddAsync(newUser, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserDto>(newUser);
    }
}
