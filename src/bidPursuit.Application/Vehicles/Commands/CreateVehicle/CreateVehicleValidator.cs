using FluentValidation;

namespace bidPursuit.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleValidator()
    {
        RuleFor(v => v.Brand)
            .NotEmpty().WithMessage("Make is required.")
            .MaximumLength(50).WithMessage("Make cannot exceed 50 characters.");
        RuleFor(v => v.Model)
            .NotEmpty().WithMessage("Model is required.")
            .MaximumLength(50).WithMessage("Model cannot exceed 50 characters.");
        RuleFor(v => v.Year)
            .InclusiveBetween(1886, DateTime.Now.Year + 1).WithMessage($"Year must be between 1886 and {DateTime.Now.Year + 1}.");
        RuleFor(v => v.StartingPrice)
            .GreaterThan(0).WithMessage("Starting price must be greater than zero.");
        RuleFor(v => v.BuyNowPrice)
            .GreaterThan(v => v.StartingPrice).WithMessage("Buy Now price must be greater than Starting price.");
        RuleFor(v => v.CurrentLocation)
            .NotEmpty().WithMessage("Current location is required.")
            .MaximumLength(100).WithMessage("Current location cannot exceed 100 characters.");
    }
}
