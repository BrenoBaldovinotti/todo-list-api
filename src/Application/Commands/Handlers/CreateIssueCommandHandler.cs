using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.Handlers;

public class CreateIssueCommandHandler(IIssueRepository issueRepository) : IRequestHandler<CreateIssueCommand, Guid>
{
    public async Task<Guid> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        var issue = new Issue(request.Name);
        await issueRepository.AddIssueAsync(issue);
        return issue.Id;
    }
}
