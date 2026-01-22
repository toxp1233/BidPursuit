using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Bids.Commands.PlaceBid;

public class PlaceBidCommand : IRequest<VehicleDto>
{
    public Guid VehicleId { get; set; }
    public decimal Increment { get; set; }
    public Guid UserId { get; set; }
}
