namespace DressedUp.Domain.Aggregates.FollowAggregate.Events;

public class FollowCreatedEvent
{
    public int FollowerId { get; }
    public int FolloweeId { get; }
    public DateTime CreatedDate { get; }

    public FollowCreatedEvent(int followerId, int followeeId, DateTime createdDate)
    {
        FollowerId = followerId;
        FolloweeId = followeeId;
        CreatedDate = createdDate;
    }
}