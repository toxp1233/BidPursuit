using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Infrastructure.Repositories;

public class BidRepository(BidPursuitDbContext context) : Repository<Bid>(context), IBidRepository
{
    private readonly BidPursuitDbContext _context = context;
    public async Task<Bid?> GetHighestBidForVehicleAsync(Guid vehicleId, CancellationToken cancellationToken)
    {
        return await _context.Bids
            .Where(b => b.VehicleId == vehicleId)
            .OrderByDescending(b => b.Amount)
            .FirstOrDefaultAsync(cancellationToken);
    }

}
