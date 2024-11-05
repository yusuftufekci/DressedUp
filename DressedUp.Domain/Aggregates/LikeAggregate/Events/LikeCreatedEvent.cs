namespace DressedUp.Domain.Aggregates.LikeAggregate.Events;

public class LikeCreatedEvent
{
    public int LikeId { get; }
    public int PostId { get; }
    public int UserId { get; }
    public DateTime CreatedDate { get; }

    public LikeCreatedEvent(int likeId, int postId, int userId, DateTime createdDate)
    {
        LikeId = likeId;
        PostId = postId;
        UserId = userId;
        CreatedDate = createdDate;
    }
}