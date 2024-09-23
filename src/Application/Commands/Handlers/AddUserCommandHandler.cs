using Application.Utilities;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Handlers;

public class AddUserCommandHandler(IUserRepository userRepository) : IRequestHandler<AddUserCommand, Guid>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = PasswordHasher.HashPassword(request.Password);
        var user = new User(request.Username, passwordHash, request.Role);
        await _userRepository.AddUserAsync(user);
        return user.Id;
    }
}
