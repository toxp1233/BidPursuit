using FluentValidation;

namespace bidPursuit.Application.Vehicles.Commands.UploadVehicleImage;

public class UploadVehicleImageValidator : AbstractValidator<UploadVehicleImageCommand>
{
    public UploadVehicleImageValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("Vehicle ID is required.");
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("File name is required.");
        RuleFor(x => x.File)
            .NotNull().WithMessage("File stream is required.");
    }
}
