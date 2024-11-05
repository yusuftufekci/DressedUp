using DressedUp.Domain.Aggregates.PostAggregate;
using DressedUp.Domain.Interfaces;
using DressedUp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DressedUp.Infrastructure.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
    {
        return await _dbSet.Where(p => p.UserId == userId).ToListAsync();
    }
}