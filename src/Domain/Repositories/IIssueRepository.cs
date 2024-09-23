using Domain.Entities;

namespace Domain.Repositories;

public interface IIssueRepository
{
    Task AddIssueAsync(Issue issue);
    Task<IEnumerable<Issue>?> GetAllIssuesAsync();
    Task<Issue?> GetIssueByIdAsync(Guid id);
    Task RemoveCompletedIssuesAsync();
    Task UpdateIssueAsync(Issue issue);
}
