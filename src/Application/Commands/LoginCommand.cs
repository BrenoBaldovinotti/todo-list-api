using MediatR;

namespace Application.Commands;

public class LoginCommand(string username, string password) : IRequest<string>
{
    public string Username { get; } = username;
    public string Password { get; } = password;
}
