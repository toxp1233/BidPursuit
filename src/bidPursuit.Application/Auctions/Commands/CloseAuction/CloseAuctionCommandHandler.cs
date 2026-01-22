using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Enums;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auctions.Commands.CloseAuction;

public class CloseAuctionCommandHandler(
    IAuctionRepository auctionRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<CloseAuctionCommand>
{
    public async Task Handle(CloseAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await auctionRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Auction), request.Id.ToString());

        if (auction.State == AuctionState.Closed)
            throw new InvalidBusinessOperationException("Auction is already closed.");

        if (auction.State != AuctionState.InProgress)
            throw new InvalidBusinessOperationException("Only auctions in progress can be closed.");

        auction.State = AuctionState.Closed;
        auction.UpdatedAt = DateTime.UtcNow;

        auctionRepository.Update(auction);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
