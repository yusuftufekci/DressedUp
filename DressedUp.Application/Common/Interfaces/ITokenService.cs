using DressedUp.Domain.Aggregates.UserAggregate;

namespace DressedUp.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, string ipAddress, string deviceId);
    RefreshToken GenerateRefreshToken(int userId, string ipAddress, string deviceId);
}