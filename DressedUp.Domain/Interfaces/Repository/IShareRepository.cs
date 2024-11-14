using DressedUp.Domain.Aggregates.ShareAggregate;

namespace DressedUp.Domain.Interfaces;

public interface IShareRepository : IGenericRepository<Share>
{
    Task<IEnumerable<Share>> GetSharesByUserIdAsync(int userId);
}