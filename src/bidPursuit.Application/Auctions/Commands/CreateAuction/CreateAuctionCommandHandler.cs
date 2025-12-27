using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auctions.Commands.CreateAuction;

public class CreateAuctionCommandHandler(
    IAuctionRepository auctionRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<CreateAuctionCommand, AuctionDto>
{
    public async Task<AuctionDto> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var newAction = mapper.Map<Auction>(request);
        await auctionRepository.AddAsync(newAction, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<AuctionDto>(newAction);
    }
}
