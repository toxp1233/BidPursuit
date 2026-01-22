using bidPursuit.Domain.Enums;

namespace bidPursuit.Application.PublicDtos;

public class AuctionDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AuctionState State { get; set; }
    public string Title { get; set; } = default!;
    public bool IsActive { get; set; }

    public string Location { get; set; } = default!;
    public string Description { get; set; } = default!;

    public UserDto Organizer { get; set; } = default!;
    public ICollection<VehicleDto> Vehicles { get; set; } = [];
}
