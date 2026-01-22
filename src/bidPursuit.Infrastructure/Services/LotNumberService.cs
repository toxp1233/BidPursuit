using bidPursuit.Domain.Services;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Infrastructure.Services;

public class LotNumberService(BidPursuitDbContext context) : ILotNumberService
{
    public async Task<int> GenerateAsync(Guid auctionId)
    {
        var max = await context.Vehicles
            .Where(v => v.AuctionId == auctionId)
            .MaxAsync(v => (int?)v.LotNumber);

        return (max ?? 0) + 1;
    }
}
