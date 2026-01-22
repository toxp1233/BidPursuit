using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteVehicleCommand>
{
    public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Vehicle), request.Id.ToString());
        if (vehicle.IsSold)
        {
            throw new InvalidBusinessOperationException("Cannot delete a sold vehicle.");
        }
        else if (vehicle.Auction != null && vehicle.Auction.IsActive)
        {
            throw new InvalidBusinessOperationException("Cannot delete a vehicle that is part of an active auction.");
        }
        else if (vehicle.Bids != null && vehicle.Bids.Count != 0)
        {
            throw new InvalidBusinessOperationException("Cannot delete a vehicle that has bids placed on it.");

        }

        vehicleRepository.Delete(vehicle);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
