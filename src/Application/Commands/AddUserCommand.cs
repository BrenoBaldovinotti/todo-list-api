using Domain.Enum;
using MediatR;

namespace Application.Commands;

public class AddUserCommand(string username, string password, UserRole role) : IRequest<Guid>
{
    public string Username { get; } = username;
    public string Password { get; } = password;
    public UserRole Role { get; } = role;
}
