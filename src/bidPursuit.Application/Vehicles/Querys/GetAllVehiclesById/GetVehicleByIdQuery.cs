using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Vehicles.Querys.GetAllVehiclesById;

public record GetVehicleByIdQuery(Guid Id) : IRequest<VehicleDto>;
