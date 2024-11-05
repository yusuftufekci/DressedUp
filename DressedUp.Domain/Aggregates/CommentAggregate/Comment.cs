namespace DressedUp.Domain.Aggregates.CommentAggregate;

public class Comment
{
    public int CommentId { get; private set; }
    public int PostId { get; private set; }
    public int UserId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Comment(int postId, int userId, string content)
    {
        PostId = postId;
        UserId = userId;
        Content = content;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
