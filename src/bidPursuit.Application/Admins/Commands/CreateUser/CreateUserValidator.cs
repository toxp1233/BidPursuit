using FluentValidation;

namespace bidPursuit.Application.Admins.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.UserForCreation.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(u => u.UserForCreation.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");

        RuleFor(u => u.UserForCreation.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(u => u.UserForCreation.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(u => u.Roles)
            .NotEmpty().WithMessage("Role is required.")
            .IsInEnum().WithMessage("Role must be a valid enum value.");
    }
}
