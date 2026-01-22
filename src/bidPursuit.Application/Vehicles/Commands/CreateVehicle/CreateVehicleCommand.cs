using MediatR;

namespace bidPursuit.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommand : IRequest
{
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }
    public decimal StartingPrice { get; set; }
    public decimal BuyNowPrice { get; set; }
    public string CurrentLocation { get; set; } = default!;
    public Guid UserId { get; set; }
};

