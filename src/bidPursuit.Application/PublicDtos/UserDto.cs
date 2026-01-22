namespace bidPursuit.Application.PublicDtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Country { get; set; } = default!;
}
