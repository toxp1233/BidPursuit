using FluentValidation;

namespace bidPursuit.Application.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleValidator : AbstractValidator<DeleteVehicleCommand>
{
    public DeleteVehicleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Vehicle ID must not be empty.");
    }
}
