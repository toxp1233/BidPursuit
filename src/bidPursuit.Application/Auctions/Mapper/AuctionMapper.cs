using AutoMapper;
using bidPursuit.Application.Auctions.Commands.CreateAuction;
using bidPursuit.Domain.Entities;
namespace bidPursuit.Application.Auctions.Mapper;

public class AuctionMapper : Profile
{
    public AuctionMapper()
    {
        CreateMap<CreateAuctionCommand, Auction>();
    }
}
