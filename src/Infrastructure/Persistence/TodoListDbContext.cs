using Application.Utilities;
using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class TodoListDbContext(DbContextOptions<TodoListDbContext> options) : DbContext(options)
{
    public DbSet<Issue> Issues { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).IsRequired();
        });

        SeedAdminUser(modelBuilder);
    }

    private static void SeedAdminUser(ModelBuilder modelBuilder)
    {
        var adminUserId = Guid.NewGuid();
        var adminUser = new User(
            "admin",
            PasswordHasher.HashPassword("verylongadminpassword123456789!@#$%¨&*("),
            UserRole.Admin
        );
        modelBuilder.Entity<User>().HasData(new
        {
            Id = adminUserId,
            adminUser.Username,
            adminUser.PasswordHash,
            adminUser.Role
        });
    }
}
