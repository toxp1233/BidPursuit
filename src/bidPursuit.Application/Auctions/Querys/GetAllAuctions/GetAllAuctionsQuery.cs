using bidPursuit.Application.Common;
using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Auctions.Querys.GetAllAuctions;

public record GetAllAuctionsQuery(PaginationParameters PaginationParameters) : IRequest<PaginatedList<AuctionDto>>;
