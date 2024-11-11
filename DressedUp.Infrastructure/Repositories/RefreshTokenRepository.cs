using DressedUp.Domain.Aggregates.UserAggregate;
using DressedUp.Domain.Interfaces;
using DressedUp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DressedUp.Infrastructure.Repositories;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    

    public RefreshTokenRepository(AppDbContext context) : base(context) { }


    // Token değerine göre bir refresh token bulur
    public async Task<RefreshToken> GetByTokenAsync(string token)
    {
        return await _dbSet
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    // Kullanıcı ID'sine göre tüm refresh token'ları getirir
    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(rt => rt.UserId == userId)
            .ToListAsync();
    }

    // Kullanıcı ID'sine göre tüm refresh token'ları siler
    public async Task DeleteAllByUserIdAsync(int userId)
    {
        var tokens = await _dbSet
            .Where(rt => rt.UserId == userId)
            .ToListAsync();

        _dbSet.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }
}