using bidPursuit.Domain.Entities;

namespace bidPursuit.Domain.Interfaces;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetVehicleById(Guid id, CancellationToken cancellationToken);
    Task<Vehicle?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken);
}
