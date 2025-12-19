using AutoMapper;
using bidPursuit.Application.Auth.Dtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auth.Commands.Register;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IJwtService jwtService,
    IPasswordHelper passwordHelper,
    IMapper mapper
    ) : IRequestHandler<RegisterCommand, TokenDto>
{
    public async Task<TokenDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.GetByNameAsync(request.RegistrationParameters.Name, cancellationToken) is not null)
            throw new AlreadyExistsException(nameof(User), request.RegistrationParameters.Name);

        if (await userRepository.GetByEmailAsync(request.RegistrationParameters.Email, cancellationToken) is not null)
            throw new AlreadyExistsException(nameof(User), request.RegistrationParameters.Email);

        var newUser = mapper.Map<User>(request.RegistrationParameters);

        newUser.PasswordHash = passwordHelper.Hash(request.RegistrationParameters.Password);
        newUser.RefreshToken = jwtService.GenerateRefreshToken();
        newUser.RefreshTokenExpiryTime = jwtService.GenerateRefreshTokenExpiry();

        await userRepository.AddAsync(newUser, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var tokens = mapper.Map<TokenDto>(newUser);
        tokens.Token = jwtService.GenerateToken(newUser);

        return tokens;
    }
}
