namespace DressedUp.Domain.Aggregates.LikeAggregate;

public class Like
{
    public int LikeId { get; private set; }
    public int PostId { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Like(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
}