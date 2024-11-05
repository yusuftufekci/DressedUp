using DressedUp.Domain.Aggregates.FollowAggregate;
using DressedUp.Domain.Interfaces;
using DressedUp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DressedUp.Infrastructure.Repositories;

public class FollowRepository : GenericRepository<Follow>, IFollowRepository
{
    public FollowRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Follow>> GetFollowersAsync(int userId)
    {
        return await _dbSet.Where(f => f.FolloweeId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Follow>> GetFollowingAsync(int userId)
    {
        return await _dbSet.Where(f => f.FollowerId == userId).ToListAsync();
    }
}