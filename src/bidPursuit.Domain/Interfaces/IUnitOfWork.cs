namespace bidPursuit.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IAuctionRepository Auctions { get; }
    IVehicleRepository Vehicles { get; }
    IBidRepository Bids { get; }
    IAuctionParticipantRepository AuctionParticipants { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
