using DressedUp.Domain.Aggregates.UserAggregate;

namespace DressedUp.Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}