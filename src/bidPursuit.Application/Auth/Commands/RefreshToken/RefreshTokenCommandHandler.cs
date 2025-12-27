using AutoMapper;
using bidPursuit.Application.Auth.Dtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUserRepository userRepository,
    IMapper mapper,
    IJwtService jwtService
    ) : IRequestHandler<RefreshTokenCommand, TokenDto>
{
    public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw new NotFoundException(nameof(User), request.UserId.ToString());
        var tokens = mapper.Map<TokenDto>(user);
        tokens.Token = jwtService.ValidateRefreshToken(request.RefreshToken, user);
        return tokens;
    }
}
