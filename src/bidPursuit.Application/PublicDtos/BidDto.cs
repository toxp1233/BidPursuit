namespace bidPursuit.Application.PublicDtos;

public class BidDto
{
    public Guid Id { get; set; }            // Primary Key
    public Guid VehicleId { get; set; }     // Foreign Key
    public Guid UserId { get; set; }        // Who made the bid
    public decimal Amount { get; set; }     // Bid amount
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public VehicleDto Vehicle { get; set; } = default!;
    public UserDto User { get; set; } = default!;
}
