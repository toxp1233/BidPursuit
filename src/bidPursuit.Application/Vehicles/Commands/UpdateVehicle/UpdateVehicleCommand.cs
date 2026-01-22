using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? BuyNowPrice { get; set; }
};
