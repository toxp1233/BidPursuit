using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;

namespace bidPursuit.Application.PublicMapper;

public class UniversalMapper : Profile
{
    public UniversalMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
        CreateMap<Bid, BidDto>().ReverseMap();
        CreateMap<Auction, AuctionDto>().ReverseMap();
        CreateMap<AuctionParticipant, AuctionParticipantDto>().ReverseMap();
    }
}
