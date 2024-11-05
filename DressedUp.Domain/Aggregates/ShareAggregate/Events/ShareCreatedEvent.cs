namespace DressedUp.Domain.Aggregates.ShareAggregate.Events;

public class ShareCreatedEvent
{
    public int ShareId { get; }
    public int PostId { get; }
    public int UserId { get; }
    public DateTime CreatedDate { get; }

    public ShareCreatedEvent(int shareId, int postId, int userId, DateTime createdDate)
    {
        ShareId = shareId;
        PostId = postId;
        UserId = userId;
        CreatedDate = createdDate;
    }
}