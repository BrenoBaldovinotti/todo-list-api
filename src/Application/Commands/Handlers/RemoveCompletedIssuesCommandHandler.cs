using Domain.Repositories;
using MediatR;

namespace Application.Commands.Handlers;

public class RemoveCompletedIssuesCommandHandler(IIssueRepository issueRepository) : IRequestHandler<RemoveCompletedIssuesCommand, Unit>
{
    public async Task<Unit> Handle(RemoveCompletedIssuesCommand request, CancellationToken cancellationToken)
    {
        await issueRepository.RemoveCompletedIssuesAsync();
        return Unit.Value;
    }
}
