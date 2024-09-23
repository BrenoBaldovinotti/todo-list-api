using Domain.Enum;

namespace Domain.Entities;

public class User(string username, string passwordHash, UserRole role)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = username;
    public string PasswordHash { get; private set; } = passwordHash;
    public UserRole Role { get; private set; } = role;

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    public void UpdateRole(UserRole role)
    {
        Role = role;
    }
}
