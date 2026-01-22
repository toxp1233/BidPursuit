using AutoMapper;
using AutoMapper.QueryableExtensions;
using bidPursuit.Application.Common;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Admins.Querys.GetAllUsers;

public class GetAllUsersQueryHandler(
    IUserRepository userRepository,
    IMapper mapper
    ) : IRequestHandler<GetAllUsersQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = userRepository.Query();

        if (!string.IsNullOrEmpty(request.PaginationParameters.Search))
        {
            var search = request.PaginationParameters.Search.ToLower();
            users = users.Where(u =>
                u.Name.ToLower().Contains(search) ||
                u.Email.ToLower().Contains(search) ||
                u.Id.ToString().Contains(search)
            );
        }
        var userDto = users.ProjectTo<UserDto>(mapper.ConfigurationProvider);
        return await PaginatedList<UserDto>.CreateAsync(
            userDto,
            request.PaginationParameters.PageNumber,
            request.PaginationParameters.PageSize
        );

    }
}
