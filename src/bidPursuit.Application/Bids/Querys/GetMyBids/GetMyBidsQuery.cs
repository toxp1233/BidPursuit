using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Bids.Querys.GetMyBids;

public record GetMyBidsQuery(Guid UserId) : IRequest<List<BidDto>>;

