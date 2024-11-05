namespace DressedUp.Domain.Aggregates.UserAggregate.Events;

public class UserUpdatedEvent
{
    public int UserId { get; }
    public DateTime UpdatedDate { get; }

    public UserUpdatedEvent(int userId, DateTime updatedDate)
    {
        UserId = userId;
        UpdatedDate = updatedDate;
    }
}