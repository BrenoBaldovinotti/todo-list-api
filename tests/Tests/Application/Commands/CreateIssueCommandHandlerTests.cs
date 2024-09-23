using Application.Commands.Handlers;
using Application.Commands;
using Domain.Repositories;
using Moq;
using Domain.Entities;

namespace Tests.Application.Commands;

public class CreateIssueCommandHandlerTests
{
    private readonly Mock<IIssueRepository> _issueRepositoryMock;
    private readonly CreateIssueCommandHandler _handler;

    public CreateIssueCommandHandlerTests()
    {
        _issueRepositoryMock = new Mock<IIssueRepository>();
        _handler = new CreateIssueCommandHandler(_issueRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateIssue()
    {
        // Arrange
        var command = new CreateIssueCommand("New Issue");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _issueRepositoryMock.Verify(repo => repo.AddIssueAsync(It.IsAny<Issue>()), Times.Once);
        Assert.IsType<Guid>(result);
    }
}