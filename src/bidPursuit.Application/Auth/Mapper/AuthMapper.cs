using AutoMapper;
using bidPursuit.Application.Auth.Dtos;
using bidPursuit.Domain.Entities;

namespace bidPursuit.Application.Auth.Mapper;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<User, TokenDto>();
        CreateMap<RegisterDto, User>();    
    }
}
