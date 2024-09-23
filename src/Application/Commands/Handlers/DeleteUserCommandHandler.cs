using Domain.Repositories;
using MediatR;

namespace Application.Commands.Handlers;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _ = await userRepository.GetUserByIdAsync(request.UserId) ?? throw new Exception("User not found.");

        await userRepository.DeleteUserAsync(request.UserId);
        
        return Unit.Value;
    }
}
