using Microsoft.AspNetCore.Http;

namespace bidPursuit.Application.Vehicles.Dtos;

public class UploadVehicleImageRequestDto
{
    public IFormFile File { get; set; } = default!;
}
