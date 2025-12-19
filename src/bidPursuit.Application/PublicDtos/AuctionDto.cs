namespace bidPursuit.Application.PublicDtos;

public class AuctionDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; } = default!;
    public ICollection<AuctionParticipantDto> Participants { get; set; } = [];
    public ICollection<VehicleDto> Vehicles { get; set; } = [];
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Location { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid OrganizerId { get; set; }
    public UserDto Organizer { get; set; } = default!;
}
