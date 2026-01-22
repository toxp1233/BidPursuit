using AutoMapper;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommandHandler(
    IMapper mapper,
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateVehicleCommand>
{
    public async Task Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
       await vehicleRepository.AddAsync(mapper.Map<Vehicle>(request), cancellationToken);
       await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
