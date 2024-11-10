using DressedUp.Domain.Aggregates.UserAggregate;

namespace DressedUp.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, string ipAddress);
    RefreshToken GenerateRefreshToken(int userId, string ipAddress);
}