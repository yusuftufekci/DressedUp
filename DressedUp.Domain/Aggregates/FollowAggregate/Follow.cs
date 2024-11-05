namespace DressedUp.Domain.Aggregates.FollowAggregate;

public class Follow
{
    public int FollowerId { get; private set; }
    public int FolloweeId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Follow(int followerId, int followeeId)
    {
        FollowerId = followerId;
        FolloweeId = followeeId;
        CreatedAt = DateTime.UtcNow;
    }
}