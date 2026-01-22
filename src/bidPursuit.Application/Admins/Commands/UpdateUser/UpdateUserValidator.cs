using FluentValidation;

namespace bidPursuit.Application.Admins.Commands.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("UserId cannot be empty.");
        RuleFor(u => u.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(u => u.Email)
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");
        RuleFor(u => u.Country)
            .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
    }
}
