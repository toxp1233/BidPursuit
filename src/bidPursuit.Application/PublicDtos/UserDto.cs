using bidPursuit.Domain.Enums;

namespace bidPursuit.Application.PublicDtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Country { get; set; } = default!;
    public Roles Role { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsEmailVerified { get; set; } = false;
    public bool IsAdmin { get; set; } = false;

    public ICollection<AuctionParticipantDto>? AuctionParticipations { get; set; } = [];
    public ICollection<VehicleDto>? PublishedVehicles { get; set; } = [];
    public ICollection<BidDto>? Bids { get; set; } = [];
    public ICollection<AuctionDto>? OrganizedAuctions { get; set; } = [];
}
