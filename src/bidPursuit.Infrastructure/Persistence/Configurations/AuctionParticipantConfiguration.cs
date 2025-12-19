using bidPursuit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bidPursuit.Infrastructure.Persistence.Configurations;

public class AuctionParticipantConfiguration : IEntityTypeConfiguration<AuctionParticipant>
{
    public void Configure(EntityTypeBuilder<AuctionParticipant> builder)
    {
        builder.ToTable("AuctionParticipants");

        builder.HasKey(ap => ap.Id);

        builder.HasIndex(ap => new { ap.UserId, ap.AuctionId })
            .IsUnique(); // prevents joining twice

        builder.HasOne(ap => ap.User)
            .WithMany(u => u.AuctionParticipations)
            .HasForeignKey(ap => ap.UserId);

        builder.HasOne(ap => ap.Auction)
            .WithMany(a => a.Participants)
            .HasForeignKey(ap => ap.AuctionId);
    }
}