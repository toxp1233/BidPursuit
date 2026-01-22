using FluentValidation;

namespace bidPursuit.Application.Auctions.Commands.CloseAuction;

public class CloseAuctionValidator : AbstractValidator<CloseAuctionCommand>
{
    public CloseAuctionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Auction Id is required.");
    }
}
