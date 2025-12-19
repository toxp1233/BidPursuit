using bidPursuit.Domain.Entities;

namespace bidPursuit.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByNameAsync(string Name, CancellationToken cancellationToken);
}
