using FluentValidation;

namespace bidPursuit.Application.Vehicles.Commands.AssignVehicleToAuction;

public class AssignVehicleToAuctionValidator : AbstractValidator<AssignVehicleToAuctionCommand>
{
    public AssignVehicleToAuctionValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("Vehicle Id must not be empty.");
        RuleFor(x => x.AuctionId)
            .NotEmpty().WithMessage("Auction Id must not be empty.");
    }
}
