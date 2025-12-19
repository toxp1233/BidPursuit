namespace bidPursuit.Application.PublicDtos;

public class AuctionParticipantDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserDto User { get; set; } = default!;

    public Guid AuctionId { get; set; }
    public AuctionDto Auction { get; set; } = default!;

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;
    public bool IsEarlyBidder { get; set; } = false;
}
