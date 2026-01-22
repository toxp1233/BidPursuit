using AutoMapper;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Enums;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auctions.Commands.UpdateAuction;

public class UpdateAuctionCommandHandler(
    IAuctionRepository auctionRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateAuctionCommand>
{
    public async Task Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await auctionRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Auction), request.Id.ToString());
        if (auction.State != AuctionState.Scheduled)
            throw new InvalidBusinessOperationException("Only scheduled auctions can be updated.");

        mapper.Map(request, auction);
        auction.UpdatedAt = DateTime.UtcNow;
        auctionRepository.Update(auction);  
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
