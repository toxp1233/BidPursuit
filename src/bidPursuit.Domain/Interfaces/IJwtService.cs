using bidPursuit.Domain.Entities;

namespace bidPursuit.Domain.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    DateTime GenerateRefreshTokenExpiry();
    string ValidateRefreshToken(string refreshToken, User user);
    string GenerateEmailVerificationToken(Guid userId);
    Guid? ValidateEmailVerificationToken(string token);
}
