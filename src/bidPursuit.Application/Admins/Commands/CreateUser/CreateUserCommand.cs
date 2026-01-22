using bidPursuit.Application.Auth.Dtos;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Enums;
using MediatR;

namespace bidPursuit.Application.Admins.Commands.CreateUser;

public record CreateUserCommand(RegisterDto UserForCreation, Roles Roles) : IRequest<UserDto>;
