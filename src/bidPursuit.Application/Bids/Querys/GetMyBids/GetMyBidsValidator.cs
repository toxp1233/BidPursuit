using FluentValidation;

namespace bidPursuit.Application.Bids.Querys.GetMyBids;

public class GetMyBidsValidator : AbstractValidator<GetMyBidsQuery>
{
    public GetMyBidsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}
