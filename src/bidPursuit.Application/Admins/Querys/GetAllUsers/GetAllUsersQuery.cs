using bidPursuit.Application.Common;
using bidPursuit.Application.PublicDtos;
using MediatR;

namespace bidPursuit.Application.Admins.Querys.GetAllUsers;

public record GetAllUsersQuery(PaginationParameters PaginationParameters) : IRequest<PaginatedList<UserDto>>;
