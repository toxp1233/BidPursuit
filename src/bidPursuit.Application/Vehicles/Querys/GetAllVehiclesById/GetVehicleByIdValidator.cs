using FluentValidation;

namespace bidPursuit.Application.Vehicles.Querys.GetAllVehiclesById;

public class GetVehicleByIdValidator : AbstractValidator<GetVehicleByIdQuery>
{
    public GetVehicleByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Vehicle Id must not be empty.");
    }
}
