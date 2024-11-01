using System.Diagnostics.CodeAnalysis;
using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi;

public class ChatbotPlugin : IChatbotPlugin
{
    public ChatbotPlugin() { }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string name, string? description = null, string version = "1.0.0", ICollection<IChatFunction>? functions = null)
    {
        Id = id;
        Name = name;
        Description = description;
        Version = version;
        Functions = functions ?? [];
    }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string name, ICollection<IChatFunction>? functions = null) : this(id, name, null, "1.0.0", functions) { }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string name, string? description = null, ICollection<IChatFunction>? functions = null) : this(id, name, description, "1.0.0", functions) { }

    public virtual required Guid Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual string Version { get; set; } = "1.0.0";

    public virtual ICollection<IChatFunction> Functions { get; set; } = [];
}
