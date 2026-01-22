namespace bidPursuit.Application.Services;
public interface IAuctionLifecycleService
{
    Task StartScheduledAuctionsIfNeededAsync(CancellationToken cancellationToken);
}
