using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(Guid id);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
}
