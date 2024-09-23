using Application.Commands.Handlers;
using Application.Commands;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Tests.Application.Commands;

public class UpdateIssueNameCommandHandlerTests
{
    private readonly Mock<IIssueRepository> _issueRepositoryMock;
    private readonly UpdateIssueNameCommandHandler _handler;

    public UpdateIssueNameCommandHandlerTests()
    {
        _issueRepositoryMock = new Mock<IIssueRepository>();
        _handler = new UpdateIssueNameCommandHandler(_issueRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateIssueName()
    {
        // Arrange
        var issueId = Guid.NewGuid();
        var newName = "Updated Issue Name";
        var issue = new Issue("Old Name");

        _issueRepositoryMock.Setup(repo => repo.GetIssueByIdAsync(issueId))
            .ReturnsAsync(issue);

        var command = new UpdateIssueNameCommand(issueId, newName);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _issueRepositoryMock.Verify(repo => repo.UpdateIssueAsync(issue), Times.Once);
        Assert.Equal(newName, issue.Name);
    }
}
