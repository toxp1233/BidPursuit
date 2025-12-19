namespace bidPursuit.Domain.Entities;

public class Bid
{
    public Guid Id { get; set; }            // Primary Key
    public Guid VehicleId { get; set; }     // Foreign Key
    public Guid UserId { get; set; }        // Who made the bid
    public decimal Amount { get; set; }     // Bid amount
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Vehicle Vehicle { get; set; } = default!;
    public User User { get; set; } = default!;
}
