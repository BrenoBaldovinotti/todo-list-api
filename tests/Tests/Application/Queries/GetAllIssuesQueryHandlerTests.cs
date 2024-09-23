using Application.Queries.Handlers;
using Application.Queries;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Tests.Application.Queries;

public class GetAllIssuesQueryHandlerTests
{
    private readonly Mock<IIssueRepository> _issueRepositoryMock;
    private readonly GetAllIssuesQueryHandler _handler;

    public GetAllIssuesQueryHandlerTests()
    {
        _issueRepositoryMock = new Mock<IIssueRepository>();
        _handler = new GetAllIssuesQueryHandler(_issueRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllIssues()
    {
        // Arrange
        var issues = new List<Issue>
        {
            new("Issue 1"),
            new("Issue 2"),
            new("Issue 3")
        };

        _issueRepositoryMock.Setup(repo => repo.GetAllIssuesAsync()).ReturnsAsync(issues);

        var query = new GetAllIssuesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(3, result?.Count());
        _issueRepositoryMock.Verify(repo => repo.GetAllIssuesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoIssuesExist()
    {
        // Arrange
        _issueRepositoryMock.Setup(repo => repo.GetAllIssuesAsync()).ReturnsAsync([]);

        var query = new GetAllIssuesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result!);
    }
}
