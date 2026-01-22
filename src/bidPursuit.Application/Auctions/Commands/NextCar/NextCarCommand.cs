using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Auctions.Commands.NextCar;

public record NextCarCommand(Guid AuctionId) : IRequest<VehicleDto>;
