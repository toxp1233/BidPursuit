using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace bidPursuit.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BidPursuitDbContext _db;
    private IDbContextTransaction? _transaction;
    public UnitOfWork(BidPursuitDbContext db)
    {
        _db = db;
        Users = new UserRepository(_db);
        Auctions = new AuctionRepository(_db);
        Vehicles = new VehicleRepository(_db);
        Bids = new BidRepository(_db);
        AuctionParticipants = new AuctionParticipantRepository(_db);
    }

    public IUserRepository Users { get; }
    public IAuctionRepository Auctions { get; }
    public IVehicleRepository Vehicles { get; }
    public IBidRepository Bids { get; }
    public IAuctionParticipantRepository AuctionParticipants { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _db.SaveChangesAsync(cancellationToken);
    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        if (_transaction != null) return; // already started
        _transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        if (_transaction == null) return;

        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (_transaction == null) return;

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}
