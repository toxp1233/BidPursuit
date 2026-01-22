using MediatR;

namespace bidPursuit.Application.Auctions.Commands.UpdateAuction;

public class UpdateAuctionCommand : IRequest
{
    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string Title { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Description { get; set; } = default!;

    public bool IsActive { get; set; }
}
