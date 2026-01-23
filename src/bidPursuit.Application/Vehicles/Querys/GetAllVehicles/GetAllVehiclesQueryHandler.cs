using bidPursuit.Application.Common;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Domain.Services;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace bidPursuit.Application.Vehicles.Querys.GetAllVehicles;

public class GetAllVehiclesQueryHandler(
    IMapper mapper,
    IVehicleRepository vehicleRepository,
    IBlobStorageService blobStorageService
    ) : IRequestHandler<GetAllVehiclesQuery, PaginatedList<VehicleDto>>
{
    public async Task<PaginatedList<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        // 1️⃣ Get the IQueryable from repository
        var vehicles = vehicleRepository.Query();

        // 2️⃣ Apply search filter if provided
        if (!string.IsNullOrEmpty(request.PaginationParameters.Search))
        {
            var search = request.PaginationParameters.Search.ToLower();
            vehicles = vehicles.Where(v =>
                v.Brand.ToLower().Contains(search) ||
                v.Model.ToLower().Contains(search) ||
                v.CurrentLocation.ToLower().Contains(search)
            );
        }

        // 3️⃣ Project to DTOs but keep as IQueryable for EF async operations
        var vehicleDtoQuery = vehicles.ProjectTo<VehicleDto>(mapper.ConfigurationProvider);

        // 4️⃣ Execute pagination first (EF async, memory-efficient)
        var paginatedVehicles = await PaginatedList<VehicleDto>.CreateAsync(
            vehicleDtoQuery,
            request.PaginationParameters.PageNumber,
            request.PaginationParameters.PageSize
        );

        // 5️⃣ Generate SAS URLs for each vehicle after pagination
        foreach (var vehicleDto in paginatedVehicles)
        {
            if (vehicleDto.Images != null && vehicleDto.Images.Any())
            {
                vehicleDto.Images = vehicleDto.Images
                    .Select(img => blobStorageService.GetBlobSasUrl(img))
                    .Where(url => url != null)
                    .ToList()!;
            }
        }

        return paginatedVehicles;
    }
}
