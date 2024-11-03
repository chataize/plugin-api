using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsButton : ISettingsButton
{
    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ButtonStyle Style { get; set; }

    public virtual required Func<ValueTask> Callback { get; set; }
}
