using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Domain.Services;
using MediatR;

namespace bidPursuit.Application.Vehicles.Querys.GetAllVehiclesById;

public class GetVehicleByIdQueryHandler(
    IMapper mapper,
    IVehicleRepository vehicleRepository,
    IBlobStorageService blobStorageService
    ) : IRequestHandler<GetVehicleByIdQuery, VehicleDto>
{
    public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Vehicle), request.Id.ToString());

        var vehicleDto = mapper.Map<VehicleDto>(vehicle);

        // Generate SAS URLs for all images
        if (vehicle.Images != null && vehicle.Images.Any())
        {
            vehicleDto.Images = vehicle.Images
                .Select(img => blobStorageService.GetBlobSasUrl(img))
                .Where(url => url != null)
                .ToList()!;
        }

        return vehicleDto;
    }
}
