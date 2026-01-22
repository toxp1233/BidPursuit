using FluentValidation;

namespace bidPursuit.Application.Auctions.Commands.NextCar;

public class NextCarValidator : AbstractValidator<NextCarCommand>
{
    public NextCarValidator()
    {
        RuleFor(x => x.AuctionId)
            .NotEmpty()
            .WithMessage("AuctionId must not be empty.");
    }
}
