using AutoMapper;
using AutoMapper.QueryableExtensions;
using bidPursuit.Application.Common;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bidPursuit.Application.Auctions.Querys.GetAllAuctions;

public class GetAllAuctionsQueryHandler(
    IAuctionRepository auctionRepository,
    IMapper mapper
    ) : IRequestHandler<GetAllAuctionsQuery, PaginatedList<AuctionDto>>
{
    public async Task<PaginatedList<AuctionDto>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
    {
        var auctions = auctionRepository.Query();

        if (!string.IsNullOrEmpty(request.PaginationParameters.Search))
        {
            var search = request.PaginationParameters.Search.ToLower();
            auctions = auctions.Where(u =>
                u.Title.ToLower().Contains(search) ||
                u.Description.ToLower().Contains(search)) ?? throw new NotFoundException(nameof(Auction), request.PaginationParameters.Search);
        }

        var auctionDto = auctions.ProjectTo<AuctionDto>(mapper.ConfigurationProvider);

        return await PaginatedList<AuctionDto>.CreateAsync(auctionDto,
            request.PaginationParameters.PageNumber,
            request.PaginationParameters.PageSize);
    }
}

