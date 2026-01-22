using bidPursuit.Application.Common;
using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Vehicles.Querys.GetAllVehicles;

public record GetAllVehiclesQuery(PaginationParameters PaginationParameters) : IRequest<PaginatedList<VehicleDto>>;
