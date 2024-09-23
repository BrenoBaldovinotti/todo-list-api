using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class TodoListDbContext(DbContextOptions<TodoListDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
