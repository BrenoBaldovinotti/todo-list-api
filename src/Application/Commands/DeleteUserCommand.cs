using MediatR;

namespace Application.Commands;

public class DeleteUserCommand(Guid userId) : IRequest<Unit>
{
    public Guid UserId { get; } = userId;
}
