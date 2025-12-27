using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auctions.Querys.GetAuctionById;

public class GetAuctionByIdQueryHandler(
    IAuctionRepository auctionRepository,
    IMapper mapper
    ) : IRequestHandler<GetAuctionByIdQuery, AuctionDto>
{
    public async Task<AuctionDto> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var auction = await auctionRepository.GetAuctionById(request.AuctionId, cancellationToken) ?? throw new NotFoundException(nameof(Auction), request.AuctionId.ToString());
        return mapper.Map<AuctionDto>(auction);
    }
}
