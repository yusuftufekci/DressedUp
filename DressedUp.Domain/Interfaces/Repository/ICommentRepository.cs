using DressedUp.Domain.Aggregates.CommentAggregate;

namespace DressedUp.Domain.Interfaces;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
}
