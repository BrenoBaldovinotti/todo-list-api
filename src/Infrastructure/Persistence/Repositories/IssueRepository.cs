using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class IssueRepository(TodoListDbContext context) : IIssueRepository
{
    public async Task AddIssueAsync(Issue issue)
    {
        await context.Issues.AddAsync(issue);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Issue>?> GetAllIssuesAsync()
    {
        return await context.Issues.ToListAsync();
    }

    public async Task<Issue?> GetIssueByIdAsync(Guid id)
    {
        return await context.Issues.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task RemoveCompletedIssuesAsync()
    {
        var completedIssues = await context.Issues.Where(i => i.IsCompleted).ToListAsync();
        context.Issues.RemoveRange(completedIssues);
        await context.SaveChangesAsync();
    }

    public async Task UpdateIssueAsync(Issue issue)
    {
        context.Issues.Update(issue);
        await context.SaveChangesAsync();
    }
}
