using MediatR;

namespace Application.Commands;

public class MarkIssueAsCompletedCommand(Guid issueId) : IRequest<Unit>
{
    public Guid IssueId { get; } = issueId;
}
