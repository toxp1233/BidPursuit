namespace bidPursuit.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IAuctionRepository Auctions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
