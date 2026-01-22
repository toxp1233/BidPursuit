using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Infrastructure.Repositories;

public class VehicleRepository(BidPursuitDbContext context) : Repository<Vehicle>(context), IVehicleRepository
{
    private readonly BidPursuitDbContext _context = context;
    public async Task<Vehicle?> GetVehicleById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Vehicles
            .Include(v => v.Bids)
            .ThenInclude(b => b.User)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }
    public async Task<Vehicle?> GetByIdForUpdateAsync(
       Guid id,
       CancellationToken cancellationToken)
    {
        // FromSqlRaw cannot be used after Include; instead, use FromSqlRaw directly on DbSet
        // and then use AsQueryable().Include(...) if needed, but navigation properties may not be loaded automatically.
        // Alternatively, use a transaction with SELECT ... FOR UPDATE in raw SQL and then load related entities.

        var vehicle = await _context.Vehicles
            .FromSqlRaw(
                @"SELECT * FROM ""Vehicles"" WHERE ""Id"" = @id FOR UPDATE",
                new Npgsql.NpgsqlParameter("id", id)
            )
            .FirstOrDefaultAsync(cancellationToken);

        if (vehicle != null)
        {
            // Explicitly load Bids if needed
            await _context.Entry(vehicle)
                .Collection(v => v.Bids)
                .LoadAsync(cancellationToken);
        }

        return vehicle;
    }
}
