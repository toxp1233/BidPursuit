using bidPursuit.Domain.Entities;

namespace bidPursuit.Domain.Interfaces;

public interface IAuctionParticipantRepository : IRepository<AuctionParticipant>
{
    Task<AuctionParticipant?> GetByAuctionAndUserAsync(Guid auctionId, Guid userId, CancellationToken cancellationToken);
}
