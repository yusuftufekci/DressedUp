namespace DressedUp.Domain.Aggregates.PostAggregate.Events;

public class PostCreatedEvent
{
    public int PostId { get; }
    public int UserId { get; }
    public DateTime CreatedDate { get; }

    public PostCreatedEvent(int postId, int userId, DateTime createdDate)
    {
        PostId = postId;
        UserId = userId;
        CreatedDate = createdDate;
    }
}