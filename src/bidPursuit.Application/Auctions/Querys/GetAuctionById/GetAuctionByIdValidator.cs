using FluentValidation;

namespace bidPursuit.Application.Auctions.Querys.GetAuctionById;

public class GetAuctionByIdValidator : AbstractValidator<GetAuctionByIdQuery>
{
    public GetAuctionByIdValidator()
    {
        RuleFor(x => x.AuctionId)
            .NotEmpty().WithMessage("AuctionId is required.");
    }
}
