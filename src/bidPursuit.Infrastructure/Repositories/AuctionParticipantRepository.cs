using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Infrastructure.Repositories;

public class AuctionParticipantRepository(BidPursuitDbContext context) : Repository<AuctionParticipant>(context), IAuctionParticipantRepository
{
    private readonly BidPursuitDbContext _context = context;
    public async Task<AuctionParticipant?> GetByAuctionAndUserAsync(Guid auctionId, Guid userId, CancellationToken cancellationToken)
    {
        return await _context.AuctionParticipants
        .FirstOrDefaultAsync(p => p.AuctionId == auctionId && p.UserId == userId, cancellationToken);
    }
}
