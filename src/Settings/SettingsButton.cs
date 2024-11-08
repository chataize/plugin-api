using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SettingsButton : ISettingsButton
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ButtonStyle Style { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual Func<ValueTask> Callback { get; set; } = () => ValueTask.CompletedTask;
}
