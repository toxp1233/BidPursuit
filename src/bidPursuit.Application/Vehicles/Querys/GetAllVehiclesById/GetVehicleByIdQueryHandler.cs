using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Vehicles.Querys.GetAllVehiclesById;

public class GetVehicleByIdQueryHandler(
    IMapper mapper,
    IVehicleRepository vehicleRepository
    ) : IRequestHandler<GetVehicleByIdQuery, VehicleDto>
{
    public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Vehicle), request.Id.ToString());

        return mapper.Map<VehicleDto>(vehicle);
    }
}
