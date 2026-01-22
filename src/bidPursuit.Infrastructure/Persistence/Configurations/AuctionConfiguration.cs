using bidPursuit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bidPursuit.Infrastructure.Persistence.Configurations;

public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        builder.ToTable("Auctions");

        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Organizer)
            .WithMany(u => u.OrganizedAuctions)
            .HasForeignKey(a => a.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Participants)
            .WithOne(ap => ap.Auction)
            .HasForeignKey(ap => ap.AuctionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.Xmin)
            .IsRowVersion()
            .ValueGeneratedOnAddOrUpdate();
    }
}