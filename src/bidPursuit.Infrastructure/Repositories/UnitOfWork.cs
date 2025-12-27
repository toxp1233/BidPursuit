using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;

namespace bidPursuit.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BidPursuitDbContext _db;

    public UnitOfWork(BidPursuitDbContext db)
    {
        _db = db;
        Users = new UserRepository(_db);
        Auctions = new AuctionRepository(_db);
    }

    public IUserRepository Users { get; }
    public IAuctionRepository Auctions { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _db.SaveChangesAsync(cancellationToken);
}
