using FluentValidation;

namespace bidPursuit.Application.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Vehicle Id must not be empty.");
        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand must not be empty.")
            .MaximumLength(100).WithMessage("Brand must not exceed 100 characters.");
        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model must not be empty.")
            .MaximumLength(100).WithMessage("Model must not exceed 100 characters.");
        RuleFor(x => x.Year)
            .InclusiveBetween(1886, DateTime.UtcNow.Year + 1).WithMessage("Year must be between 1886 and next year.");
        RuleFor(x => x.BuyNowPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Starting Price must be non-negative.");
    }
}
