using FluentValidation;

namespace bidPursuit.Application.Auctions.Commands.UpdateAuction;

public class UpdateAuctionValidator : AbstractValidator<UpdateAuctionCommand>
{
    public UpdateAuctionValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow);

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("EndTime must be after StartTime");
    }
}
