using Domain.Repositories;
using MediatR;

namespace Application.Commands.Handlers;

public class MarkIssueAsCompletedCommandHandler(IIssueRepository issueRepository) : IRequestHandler<MarkIssueAsCompletedCommand, Unit>
{
    public async Task<Unit> Handle(MarkIssueAsCompletedCommand request, CancellationToken cancellationToken)
    {
        var issue = await issueRepository.GetIssueByIdAsync(request.IssueId) ?? throw new Exception("Issue not found.");

        issue.MarkAsCompleted();

        await issueRepository.UpdateIssueAsync(issue);

        return Unit.Value;
    }
}
