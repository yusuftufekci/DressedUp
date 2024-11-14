using DressedUp.Domain.Aggregates.NotificationAggregate;

namespace DressedUp.Domain.Interfaces;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId);
}