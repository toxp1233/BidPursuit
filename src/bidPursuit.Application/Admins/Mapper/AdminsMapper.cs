using AutoMapper;
using bidPursuit.Application.Admins.Commands.CreateUser;
using bidPursuit.Application.Admins.Commands.UpdateUser;
using bidPursuit.Domain.Entities;

namespace bidPursuit.Application.Admins.Mapper;

public class AdminsMapper : Profile
{
    public AdminsMapper()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
}
