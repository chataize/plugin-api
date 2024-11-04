using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi;

public class ChatbotPlugin : IChatbotPlugin
{
    public ChatbotPlugin() { }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string name, ICollection<IChatFunction>? functions = null)
    {
        Id = id;
        Name = name;
        Functions = functions ?? [];
    }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string name, params IChatFunction[] functions)
    {
        Id = id;
        Name = name;
        Functions = functions;
    }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string name, string? description, ICollection<IPluginSetting>? settings = null, ICollection<IChatFunction>? functions = null)
    {
        Id = id;
        Name = name;
        Description = description;
        Settings = settings ?? [];
        Functions = functions ?? [];
    }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string name, string? description = null, string version = "1.0.0", ICollection<IPluginSetting>? settings = null, ICollection<IChatFunction>? functions = null)
    {
        Id = id;
        Name = name;
        Description = description;
        Version = version;
        Settings = settings ?? [];
        Functions = functions ?? [];
    }

    public virtual required Guid Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual string Version { get; set; } = "1.0.0";

    public virtual ICollection<IPluginSetting> Settings { get; set; } = [];

    public virtual ICollection<IChatFunction> Functions { get; set; } = [];
}
