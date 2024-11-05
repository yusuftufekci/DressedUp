namespace DressedUp.Domain.Aggregates.NotificationAggregate;

public class NotificationType
{
    public int NotificationTypeId { get; private set; }
    public string Name { get; private set; }  // Bildirim türünün adı (örneğin "Comment", "Like")
    public string Description { get; private set; }  // Bildirim türünün açıklaması

    public NotificationType(string name, string description)
    {
        Name = name;
        Description = description;
    }
}