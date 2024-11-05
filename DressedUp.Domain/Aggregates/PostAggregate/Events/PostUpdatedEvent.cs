namespace DressedUp.Domain.Aggregates.PostAggregate.Events;

public class PostUpdatedEvent
{
    public int PostId { get; }
    public DateTime UpdatedDate { get; }

    public PostUpdatedEvent(int postId, DateTime updatedDate)
    {
        PostId = postId;
        UpdatedDate = updatedDate;
    }
}