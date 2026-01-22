using bidPursuit.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Application.Services;

public class AuctionLifecycleService(
    IAuctionRepository auctionRepository,
    IUnitOfWork unitOfWork) : IAuctionLifecycleService
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task StartScheduledAuctionsIfNeededAsync(CancellationToken cancellationToken)
    {
        try
        {
            var now = DateTime.UtcNow;

            var scheduledAuctions = await _auctionRepository
                .GetScheduledAuctionsStartingBeforeAsync(now, cancellationToken);

            if (!scheduledAuctions.Any())
                return;

            foreach (var auction in scheduledAuctions)
            {
                auction.StartIfReady(now);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        } catch (DbUpdateConcurrencyException)
        {
            // Concurrency conflict occurred, likely due to another process updating the same auctions.
            // Log the exception if necessary, but we can safely ignore it here.
        }
    }
}

