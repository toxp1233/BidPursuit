using FluentValidation;

namespace bidPursuit.Application.Auctions.Commands.JoinAuction;

public class JoinAuctionValidator : AbstractValidator<JoinAuctionCommand>
{
    public JoinAuctionValidator()
    {
        RuleFor(x => x.AuctionId)
            .NotEmpty().WithMessage("Auction ID is required.");
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
