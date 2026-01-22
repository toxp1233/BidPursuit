using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Auctions.Commands.JoinAuction;

public record JoinAuctionCommand(Guid AuctionId, Guid UserId) : IRequest<AuctionDto>;
