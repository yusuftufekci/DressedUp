using DressedUp.Domain.Aggregates.FollowAggregate;

namespace DressedUp.Domain.Interfaces;

public interface IFollowRepository : IGenericRepository<Follow>
{
    Task<IEnumerable<Follow>> GetFollowersAsync(int userId);
    Task<IEnumerable<Follow>> GetFollowingAsync(int userId);
}