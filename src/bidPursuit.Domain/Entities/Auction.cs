namespace bidPursuit.Domain.Entities;

public class Auction
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; } = default!;
    public ICollection<AuctionParticipant> Participants { get; set; } = [];
    public ICollection<Vehicle> Vehicles { get; set; } = [];
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Location { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid OrganizerId { get; set; }
    public User Organizer { get; set; } = default!; 
}
