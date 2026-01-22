using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;

namespace bidPursuit.Application.PublicMapper;

public class UniversalMapper : Profile
{
    public UniversalMapper()
    {
        CreateMap<User, UserDto>();
        CreateMap<Vehicle, VehicleDto>();
        CreateMap<Auction, AuctionDto>();
        CreateMap<AuctionParticipant, AuctionParticipantDto>();
        CreateMap<Bid, BidDto>();
    }
}
