namespace DressedUp.Domain.Aggregates.NotificationAggregate.Events;

public class NotificationCreatedEvent
{
    public int NotificationId { get; }
    public int UserId { get; }
    public DateTime CreatedDate { get; }

    public NotificationCreatedEvent(int notificationId, int userId, DateTime createdDate)
    {
        NotificationId = notificationId;
        UserId = userId;
        CreatedDate = createdDate;
    }
}