using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Enums;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Infrastructure.Repositories;

public class AuctionRepository(BidPursuitDbContext context) : Repository<Auction>(context), IAuctionRepository
{
    private readonly BidPursuitDbContext _context = context;
    public async Task<Auction?> GetAuctionById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Auctions.FindAsync([id], cancellationToken);
    }
    public async Task<List<Auction>> GetScheduledAuctionsStartingBeforeAsync(
    DateTime now,
    CancellationToken cancellationToken)
    {
        return await _context.Auctions
            .Where(a => a.State == AuctionState.Scheduled && a.StartTime <= now)
            .ToListAsync(cancellationToken);
    }

}
