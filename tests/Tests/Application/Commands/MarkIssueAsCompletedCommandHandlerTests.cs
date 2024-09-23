using Application.Commands.Handlers;
using Application.Commands;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Tests.Application.Commands;

public class MarkIssueAsCompletedCommandHandlerTests
{
    private readonly Mock<IIssueRepository> _issueRepositoryMock;
    private readonly MarkIssueAsCompletedCommandHandler _handler;

    public MarkIssueAsCompletedCommandHandlerTests()
    {
        _issueRepositoryMock = new Mock<IIssueRepository>();
        _handler = new MarkIssueAsCompletedCommandHandler(_issueRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldMarkIssueAsCompleted()
    {
        // Arrange
        var issueId = Guid.NewGuid();
        var issue = new Issue("Test Issue");

        _issueRepositoryMock.Setup(repo => repo.GetIssueByIdAsync(issueId))
            .ReturnsAsync(issue);

        var command = new MarkIssueAsCompletedCommand(issueId);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(issue.IsCompleted); 
        _issueRepositoryMock.Verify(repo => repo.UpdateIssueAsync(issue), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenIssueNotFound()
    {
        // Arrange
        var issueId = Guid.NewGuid();

        _issueRepositoryMock.Setup(repo => repo.GetIssueByIdAsync(issueId)).ReturnsAsync((Issue)null);

        var command = new MarkIssueAsCompletedCommand(issueId);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}
