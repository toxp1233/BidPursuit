using FluentValidation;

namespace bidPursuit.Application.Admins.Querys.GetUserById;

public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID cannot be empty.");
    }
}
