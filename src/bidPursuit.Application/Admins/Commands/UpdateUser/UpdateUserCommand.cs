using MediatR;

namespace bidPursuit.Application.Admins.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Country { get; set; }
}
