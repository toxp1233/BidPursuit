using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.UploadVehicleImage;

public class UploadVehicleImageCommand : IRequest
{
    public Guid VehicleId { get; set; }
    public string FileName { get; set; } = default!;
    public Stream File { get; set; } = default!;
}
