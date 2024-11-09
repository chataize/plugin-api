using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi;

public class ChatbotPlugin : IChatbotPlugin
{
    public ChatbotPlugin() { }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string title, string? description = null, string? website = null, string? author = null, string version = "1.0.0", DateTimeOffset? releaseTime = null, DateTimeOffset? lastUpdateTime = null, Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IPluginSetting>>>? settingsCallback = null, Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IChatFunction>>>? functionsCallback = null)
    {
        Id = id;
        Title = title;
        Description = description;
        Website = website;
        Author = author;
        Version = version;
        ReleaseTime = releaseTime;
        LastUpdateTime = lastUpdateTime;
        SettingsCallback = settingsCallback;
        FunctionsCallback = functionsCallback;
    }

    public virtual required Guid Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? Website { get; set; }

    public virtual string? Author { get; set; }

    public virtual string Version { get; set; } = "1.0.0";

    public virtual DateTimeOffset? ReleaseTime { get; set; }

    public virtual DateTimeOffset? LastUpdateTime { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IPluginSetting>>>? SettingsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IChatFunction>>>? FunctionsCallback { get; set; }
}
