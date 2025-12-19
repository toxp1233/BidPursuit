using bidPursuit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bidPursuit.Infrastructure.Persistence.Configurations;


public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.StartingPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(v => v.BuyNowPrice)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(v => v.Auction)
            .WithMany(a => a.Vehicles)
            .HasForeignKey(v => v.AuctionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Bids)
            .WithOne(b => b.Vehicle)
            .HasForeignKey(b => b.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
