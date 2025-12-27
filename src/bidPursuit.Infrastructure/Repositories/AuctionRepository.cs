using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;

namespace bidPursuit.Infrastructure.Repositories;

public class AuctionRepository(BidPursuitDbContext context) : Repository<Auction>(context), IAuctionRepository
{
    private readonly BidPursuitDbContext _context = context;
    public async Task<Auction?> GetAuctionById(Guid id, CancellationToken cancellationToken)
    {
        var auction = await _context.Auctions.FindAsync([id], cancellationToken);
        return auction;
    }
}
