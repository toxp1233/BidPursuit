using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Auctions.Querys.GetAuctionById;

public record GetAuctionByIdQuery(Guid AuctionId) : IRequest<AuctionDto>;
