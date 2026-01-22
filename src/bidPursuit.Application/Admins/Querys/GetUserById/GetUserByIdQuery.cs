using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Admins.Querys.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
