using DressedUp.Domain.Aggregates.LikeAggregate;

namespace DressedUp.Domain.Interfaces;

public interface ILikeRepository : IGenericRepository<Like>
{
    Task<int> GetLikeCountByPostIdAsync(int postId);
}