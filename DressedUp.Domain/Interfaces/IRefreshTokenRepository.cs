using DressedUp.Domain.Aggregates.UserAggregate;

namespace DressedUp.Domain.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    // Token değerine göre bir refresh token bulur
    Task<RefreshToken> GetByTokenAsync(string token);

    // Kullanıcı ID'sine göre tüm refresh token'ları getirir
    Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId);

    // Kullanıcı ID'sine göre tüm refresh token'ları siler
    Task DeleteAllByUserIdAsync(int userId);
    
}