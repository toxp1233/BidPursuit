using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Infrastructure.Repositories;

public class UserRepository(BidPursuitDbContext context) : Repository<User>(context), IUserRepository
{
    private readonly BidPursuitDbContext _context = context;

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByNameAsync(string Name, CancellationToken cancellationToken)
    {
        return await _context.Users
        .FirstOrDefaultAsync(u => u.Name == Name, cancellationToken);
    }
}
 