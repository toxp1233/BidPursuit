namespace bidPursuit.Domain.Entities;

public class AuctionParticipant
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; } = default!;

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;
    public bool HasBidded { get; set; } = false;
    public bool IsEarlyBidder { get; set; } = false;
}
