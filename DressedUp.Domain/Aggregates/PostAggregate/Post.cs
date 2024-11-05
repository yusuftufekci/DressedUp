namespace DressedUp.Domain.Aggregates.PostAggregate;

public class Post
{
    public int PostId { get; private set; }
    public int UserId { get; private set; }
    public string Caption { get; private set; }
    public bool IsVideo { get; private set; }
    public string MediaUrl { get; private set; }
    public int LikeCount { get; private set; }
    public int ShareCount { get; private set; }
    public int CommentCount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Post(int userId, string caption, bool isVideo, string mediaUrl)
    {
        UserId = userId;
        Caption = caption;
        IsVideo = isVideo;
        MediaUrl = mediaUrl;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCaption(string newCaption)
    {
        Caption = newCaption;
        UpdatedAt = DateTime.UtcNow;
    }
}