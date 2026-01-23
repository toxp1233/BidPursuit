using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Domain.Services;
using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.UploadVehicleImage;

public class UploadVehicleImageCommandHandler(
    IVehicleRepository vehicleRepository,
    IBlobStorageService blobStorageServices,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UploadVehicleImageCommand>
{
    public async Task Handle(UploadVehicleImageCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken) ?? throw new NotFoundException(nameof(Vehicle), request.VehicleId.ToString());
        var vehicleUrl = await blobStorageServices.UploadToBlobAsync(request.File, request.FileName, cancellationToken);

        vehicle.Images.Add(vehicleUrl);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
