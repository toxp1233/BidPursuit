using bidPursuit.Domain.Entities;

namespace bidPursuit.Domain.Interfaces;

public interface IBidRepository : IRepository<Bid>
{
    Task<Bid?> GetHighestBidForVehicleAsync(Guid vehicleId, CancellationToken cancellationToken);
}
