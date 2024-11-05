namespace DressedUp.Domain.Aggregates.ShareAggregate;

public class Share
{
    public int ShareId { get; private set; }
    public int PostId { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Share(int postId, int userId)
    {
        PostId = postId;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
}