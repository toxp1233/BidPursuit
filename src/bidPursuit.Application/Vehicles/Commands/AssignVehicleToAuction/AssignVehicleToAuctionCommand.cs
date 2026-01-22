using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.AssignVehicleToAuction;

public record AssignVehicleToAuctionCommand(Guid VehicleId, Guid AuctionId) : IRequest;
