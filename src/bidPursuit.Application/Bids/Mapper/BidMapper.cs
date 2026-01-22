using AutoMapper;
using bidPursuit.Application.Bids.Commands.PlaceBid;
using bidPursuit.Domain.Entities;

namespace bidPursuit.Application.Bids.Mapper;

public class BidMapper : Profile
{
    public BidMapper()
    {
        CreateMap<PlaceBidCommand, Bid>();
    }
}
