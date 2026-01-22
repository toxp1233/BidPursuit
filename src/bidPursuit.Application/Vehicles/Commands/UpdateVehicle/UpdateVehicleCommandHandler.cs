using AutoMapper;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Enums;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleCommandHandler(
    IMapper mapper,
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateVehicleCommand>
{
    public async Task Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Vehicle), request.Id.ToString());
 
        if(vehicle.State != CarState.draftable || vehicle.IsSold)
        {
            throw new InvalidBusinessOperationException("Cannot update a vehicle because its either sold or in live auction");
        } else if(vehicle.Bids.Count != 0)
        {
            throw new InvalidBusinessOperationException("Cannot update a vehicle because it has existing bids");
        }
        mapper.Map(request, vehicle);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
