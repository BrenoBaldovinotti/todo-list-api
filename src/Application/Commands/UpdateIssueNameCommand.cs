using MediatR;
namespace Application.Commands;

public class UpdateIssueNameCommand(Guid issueId, string newName) : IRequest<Unit>
{
    public Guid IssueId { get; } = issueId;
    public string NewName { get; } = newName;
}
