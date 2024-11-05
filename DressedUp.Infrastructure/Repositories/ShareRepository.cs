using DressedUp.Domain.Aggregates.ShareAggregate;
using DressedUp.Domain.Interfaces;
using DressedUp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DressedUp.Infrastructure.Repositories;

public class ShareRepository : GenericRepository<Share>, IShareRepository
{
    public ShareRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Share>> GetSharesByUserIdAsync(int userId)
    {
        return await _dbSet.Where(s => s.UserId == userId).ToListAsync();
    }
}