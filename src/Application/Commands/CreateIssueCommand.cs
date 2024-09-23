using MediatR;

namespace Application.Commands;

public class CreateIssueCommand(string name) : IRequest<Guid>
{
    public string Name { get; set; } = name;
}
