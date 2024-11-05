namespace DressedUp.Domain.Aggregates.UserAggregate.Events;

public class UserCreatedEvent
{
    public int UserId { get; }
    public DateTime CreatedDate { get; }

    public UserCreatedEvent(int userId, DateTime createdDate)
    {
        UserId = userId;
        CreatedDate = createdDate;
    }
}