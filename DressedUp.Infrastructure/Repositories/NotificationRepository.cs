using DressedUp.Domain.Aggregates.NotificationAggregate;
using DressedUp.Domain.Interfaces;
using DressedUp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DressedUp.Infrastructure.Repositories;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId)
    {
        return await _dbSet.Where(n => n.UserId == userId && !n.IsRead).ToListAsync();
    }
}