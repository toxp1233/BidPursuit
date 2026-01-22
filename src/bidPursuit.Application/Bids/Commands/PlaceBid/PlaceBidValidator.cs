using FluentValidation;

namespace bidPursuit.Application.Bids.Commands.PlaceBid;

public class PlaceBidValidator : AbstractValidator<PlaceBidCommand>
{
    public PlaceBidValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("Vehicle ID is required.");
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Increment)
            .GreaterThan(0)
            .WithMessage("Bid amount must be positive.");
    }
}
