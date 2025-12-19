using bidPursuit.Domain.Enums;

namespace bidPursuit.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Country { get; set; } = default!;
    public Roles Role { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string PasswordHash { get; set; } = default!;
    public bool IsEmailVerified { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<AuctionParticipant>? AuctionParticipations { get; set; } = [];
    public ICollection<Vehicle>? PublishedVehicles { get; set; } = [];
    public ICollection<Bid>? Bids { get; set; } = [];
    public ICollection<Auction>? OrganizedAuctions { get; set; } = [];
}
