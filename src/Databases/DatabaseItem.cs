using System.Diagnostics.CodeAnalysis;

namespace ChatAIze.PluginApi.Databases;

public class DatabaseItem
{
    public DatabaseItem() { }

    [SetsRequiredMembers]
    public DatabaseItem(string title, string? description = null, Dictionary<string, string?>? properties = null)
    {
        Title = title;
        Description = description;
        Properties = properties ?? [];
    }

    public virtual Guid Id { get; set; } = Guid.CreateVersion7();

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public Dictionary<string, string?> Properties { get; set; } = [];

    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset LastUpdateTime { get; set; } = DateTimeOffset.UtcNow;
}
