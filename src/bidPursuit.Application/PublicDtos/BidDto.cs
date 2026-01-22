namespace bidPursuit.Application.PublicDtos;

public class BidDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserDto User { get; set; } = default!;
}
