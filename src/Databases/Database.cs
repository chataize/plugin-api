using ChatAIze.Abstractions.Databases;

namespace ChatAIze.PluginApi.Databases;

public class Database : IDatabase
{
    public Database() { }

    public Database(string title, string? description = null)
    {
        Title = title;
        Description = description;
        CreationTime = DateTimeOffset.UtcNow;
    }

    public virtual Guid Id { get; set; } = Guid.CreateVersion7();

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual DateTimeOffset CreationTime { get; set; }

    public virtual DateTimeOffset LastUpdateTime { get; set; } = DateTimeOffset.UtcNow;
}
