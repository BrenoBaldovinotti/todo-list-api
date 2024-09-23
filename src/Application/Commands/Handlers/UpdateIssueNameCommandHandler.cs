using Domain.Repositories;
using MediatR;

namespace Application.Commands.Handlers;

public class UpdateIssueNameCommandHandler(IIssueRepository issueRepository) : IRequestHandler<UpdateIssueNameCommand, Unit>
{
    public async Task<Unit> Handle(UpdateIssueNameCommand request, CancellationToken cancellationToken)
    {
        var issue = await issueRepository.GetIssueByIdAsync(request.IssueId) ?? throw new Exception("Issue not found.");
        issue.UpdateName(request.NewName);
        await issueRepository.UpdateIssueAsync(issue);

        return Unit.Value;
    }
}
