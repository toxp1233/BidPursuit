using AutoMapper;
using AutoMapper.QueryableExtensions;
using bidPursuit.Application.Common;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Application.Services;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auctions.Querys.GetAllAuctions;

public class GetAllAuctionsQueryHandler(
    IAuctionRepository auctionRepository,
    IAuctionLifecycleService lifecycleService,
    IMapper mapper
) : IRequestHandler<GetAllAuctionsQuery, PaginatedList<AuctionDto>>
{
    public async Task<PaginatedList<AuctionDto>> Handle(
        GetAllAuctionsQuery request,
        CancellationToken cancellationToken)
    {
        await lifecycleService.StartScheduledAuctionsIfNeededAsync(cancellationToken);

        var auctions = auctionRepository.Query();

        if (!string.IsNullOrEmpty(request.PaginationParameters.Search))
        {
            var search = request.PaginationParameters.Search.ToLower();
            auctions = auctions.Where(a =>
                a.Title.ToLower().Contains(search) ||
                a.Description.ToLower().Contains(search));
        }

        var auctionDto = auctions
            .ProjectTo<AuctionDto>(mapper.ConfigurationProvider);

        return await PaginatedList<AuctionDto>.CreateAsync(
            auctionDto,
            request.PaginationParameters.PageNumber,
            request.PaginationParameters.PageSize);
    }
}
