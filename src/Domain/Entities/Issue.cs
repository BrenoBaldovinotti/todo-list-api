namespace Domain.Entities;

public class Issue
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Issue(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Task name cannot be empty", nameof(name));

        Id = Guid.NewGuid();
        Name = name;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Task name cannot be empty", nameof(newName));

        Name = newName;
    }
}
