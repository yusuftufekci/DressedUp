using DressedUp.Domain.Aggregates.PostAggregate;

namespace DressedUp.Domain.Interfaces;

public interface IPostRepository : IGenericRepository<Post>
{
    Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
}