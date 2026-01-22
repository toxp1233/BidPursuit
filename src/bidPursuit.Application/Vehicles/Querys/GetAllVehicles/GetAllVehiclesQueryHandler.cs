using AutoMapper;
using AutoMapper.QueryableExtensions;
using bidPursuit.Application.Common;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Vehicles.Querys.GetAllVehicles;

public class GetAllVehiclesQueryHandler(
    IMapper mapper,
    IVehicleRepository vehicleRepository
    ) : IRequestHandler<GetAllVehiclesQuery, PaginatedList<VehicleDto>>
{
    public async Task<PaginatedList<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = vehicleRepository.Query();
        if (!string.IsNullOrEmpty(request.PaginationParameters.Search))
        {
            var search = request.PaginationParameters.Search.ToLower();
            vehicles = vehicles.Where(v =>
                v.Brand.ToLower().Contains(search) ||
                v.Model.ToLower().Contains(search) ||
                v.CurrentLocation.ToLower().Contains(search)
            );
        }

        var vehicleDto = vehicles.ProjectTo<VehicleDto>(mapper.ConfigurationProvider);

        return await PaginatedList<VehicleDto>.CreateAsync(
            vehicleDto,
            request.PaginationParameters.PageNumber,
            request.PaginationParameters.PageSize
        );
    }
}
