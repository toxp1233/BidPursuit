using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using bidPursuit.Domain.Enums;
using MediatR;
using bidPursuit.Domain.Services;

namespace bidPursuit.Application.Vehicles.Commands.AssignVehicleToAuction;

public class AssignVehicleToAuctionCommandHandler(
    IUnitOfWork unitOfWork,
    IVehicleRepository vehicleRepository,
    IAuctionRepository auctionRepository,
    ILotNumberService lotNumberService
    ) : IRequestHandler<AssignVehicleToAuctionCommand>
{
    public async Task Handle(AssignVehicleToAuctionCommand request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(
            request.VehicleId, cancellationToken)
            ?? throw new NotFoundException(nameof(Vehicle), request.VehicleId.ToString());

        var auction = await auctionRepository.GetByIdAsync(
            request.AuctionId, cancellationToken)
            ?? throw new NotFoundException(nameof(Auction), request.AuctionId.ToString());

        //if (auction.State != AuctionState.Scheduled)
        //    throw new InvalidOperationException("Cannot assign vehicle to a Live/Closed auction.");

        if (vehicle.State != CarState.draftable)
            throw new InvalidOperationException("Only draftable vehicles can be assigned to an auction.");

        var lotNumber = await lotNumberService.GenerateAsync(auction.Id);

        auction.AddVehicle(vehicle, lotNumber);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
