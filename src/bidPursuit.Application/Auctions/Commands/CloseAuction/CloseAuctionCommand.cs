using MediatR;

namespace bidPursuit.Application.Auctions.Commands.CloseAuction;

public record CloseAuctionCommand(Guid Id) : IRequest;
