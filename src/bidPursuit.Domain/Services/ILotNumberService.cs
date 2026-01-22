namespace bidPursuit.Domain.Services;

public interface ILotNumberService
{
   Task<int> GenerateAsync(Guid auctionId);
}
