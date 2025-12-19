using bidPursuit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Infrastructure.Persistence;

public class BidPursuitDbContext(DbContextOptions<BidPursuitDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<AuctionParticipant> AuctionParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BidPursuitDbContext).Assembly);
    }
}
