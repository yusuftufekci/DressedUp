namespace DressedUp.Domain.Aggregates.CommentAggregate.Events;

public class CommentCreatedEvent
{
    public int CommentId { get; }
    public int PostId { get; }
    public int UserId { get; }
    public string Content { get; }
    public DateTime CreatedDate { get; }

    public CommentCreatedEvent(int commentId, int postId, int userId, string content, DateTime createdDate)
    {
        CommentId = commentId;
        PostId = postId;
        UserId = userId;
        Content = content;
        CreatedDate = createdDate;
    }
}