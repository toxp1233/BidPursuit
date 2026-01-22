using AutoMapper;
using bidPursuit.Application.Auth.Dtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auth.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtService jwtService,
    IMapper mapper,
    IPasswordHelper passwordHelper,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<LoginCommand, TokenDto>
{
    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken) ?? throw new NotFoundException(nameof(User), request.Email);
        if (!user.IsActive)
        {
            throw new UnauthorizedException("User account is Banned.");
        }
        //else if (!user.IsEmailVerified)
        //{
        //    throw new UnauthorizedAccessException("Email is not verified.");
        //}
        else if (!passwordHelper.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid password");
        }

        user.RefreshToken = jwtService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = jwtService.GenerateRefreshTokenExpiry();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var tokens = mapper.Map<TokenDto>(user);
        tokens.Token = jwtService.GenerateToken(user);
        return tokens;
    }
}
