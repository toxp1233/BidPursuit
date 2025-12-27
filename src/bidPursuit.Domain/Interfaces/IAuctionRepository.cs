using bidPursuit.Domain.Entities;

namespace bidPursuit.Domain.Interfaces;

public interface IAuctionRepository : IRepository<Auction>
{
    Task<Auction?> GetAuctionById(Guid id, CancellationToken cancellationToken); 
}
