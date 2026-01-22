using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.DeleteVehicle;

public record DeleteVehicleCommand(Guid Id) : IRequest;
