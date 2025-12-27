using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Auctions.Commands.CreateAuction;

public class CreateAuctionCommand : IRequest<AuctionDto>
{
    public string Title { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public Guid OrganizerId { get; set; }
}
