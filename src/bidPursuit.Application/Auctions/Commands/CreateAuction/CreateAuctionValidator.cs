using FluentValidation;

namespace bidPursuit.Application.Auctions.Commands.CreateAuction;

public class CreateAuctionValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("StartTime is required.")
            .GreaterThan(DateTime.UtcNow)
            .LessThan(x => x.EndTime).WithMessage("StartTime must be before EndTime.");
        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("EndTime is required.")
            .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
        RuleFor(x => x.OrganizerId)
            .NotEmpty().WithMessage("OrganizerId is required.");
    }
}
