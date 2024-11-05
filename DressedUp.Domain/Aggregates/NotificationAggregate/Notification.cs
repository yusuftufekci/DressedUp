namespace DressedUp.Domain.Aggregates.NotificationAggregate;

public class Notification
{
    public int NotificationId { get; private set; }
    public int UserId { get; private set; } // Bildirim alıcı kullanıcı ID
    public int NotificationTypeId { get; private set; }
    public int? FromUserId { get; private set; } // Bildirimi oluşturan kullanıcı ID (isteğe bağlı)
    public int? PostId { get; private set; } // İlgili Post ID (isteğe bağlı)
    public int? CommentId { get; private set; } // İlgili Comment ID (isteğe bağlı)
    public int? ShareId { get; private set; } // İlgili Share ID (isteğe bağlı)
    public string Content { get; private set; } // Bildirim içeriği
    public bool IsRead { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Notification(int userId, int notificationTypeId, string content, int? fromUserId = null, int? postId = null,
        int? commentId = null, int? shareId = null)
    {
        UserId = userId;
        NotificationTypeId = notificationTypeId;
        Content = content;
        FromUserId = fromUserId;
        PostId = postId;
        CommentId = commentId;
        ShareId = shareId;
        IsRead = false;
        CreatedAt = DateTime.UtcNow;
    }

    // Bildirimi okundu olarak işaretlemek için bir metot
    public void MarkAsRead()
    {
        IsRead = true;
    }
}