using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi;

public class ChatbotPlugin : IChatbotPlugin
{
    public ChatbotPlugin() { }

    public virtual required Guid Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? Website { get; set; }

    public virtual string? Author { get; set; }

    public virtual string Version { get; set; } = "1.0.0";

    public virtual DateTimeOffset? ReleaseTime { get; set; }

    public virtual DateTimeOffset? LastUpdateTime { get; set; }

    public virtual Func<ValueTask<ICollection<IPluginSetting>>> SettingsCallback { get; set; } = () => ValueTask.FromResult<ICollection<IPluginSetting>>([]);

    public virtual Func<ValueTask<ICollection<IChatFunction>>> FunctionsCallback { get; set; } = () => ValueTask.FromResult<ICollection<IChatFunction>>([]);
}
