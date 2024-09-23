using Application.Commands.Handlers;
using Application.Commands;
using Domain.Repositories;
using Moq;

namespace Tests.Application.Commands;

public class RemoveCompletedIssuesCommandHandlerTests
{
    private readonly Mock<IIssueRepository> _issueRepositoryMock;
    private readonly RemoveCompletedIssuesCommandHandler _handler;

    public RemoveCompletedIssuesCommandHandlerTests()
    {
        _issueRepositoryMock = new Mock<IIssueRepository>();
        _handler = new RemoveCompletedIssuesCommandHandler(_issueRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldRemoveCompletedIssues()
    {
        // Arrange
        var command = new RemoveCompletedIssuesCommand();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _issueRepositoryMock.Verify(repo => repo.RemoveCompletedIssuesAsync(), Times.Once);
    }
}
