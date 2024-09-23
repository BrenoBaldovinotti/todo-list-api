using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Factories;

public class TodoListDbContextFactory : IDesignTimeDbContextFactory<TodoListDbContext>
{
    public TodoListDbContext CreateDbContext(string[] args)
    {
        // Set up configuration to read from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TodoListDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);

        return new TodoListDbContext(optionsBuilder.Options);
    }
}
