using DressedUp.Domain.Aggregates.LikeAggregate;
using DressedUp.Domain.Interfaces;
using DressedUp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DressedUp.Infrastructure.Repositories;

public class LikeRepository : GenericRepository<Like>, ILikeRepository
{
    public LikeRepository(AppDbContext context) : base(context) { }

    public async Task<int> GetLikeCountByPostIdAsync(int postId)
    {
        return await _dbSet.CountAsync(l => l.PostId == postId);
    }
}