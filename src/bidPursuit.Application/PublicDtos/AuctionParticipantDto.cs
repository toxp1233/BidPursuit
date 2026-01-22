namespace bidPursuit.Application.PublicDtos;

public class AuctionParticipantDto
{
    public Guid Id { get; set; }
    public DateTime JoinedAt { get; set; }

    public bool IsEarlyBidder { get; set; }

    public UserDto User { get; set; } = default!;
}
