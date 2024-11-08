using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SettingsButton : ISettingsButton
{
    public SettingsButton() { }

    [SetsRequiredMembers]
    public SettingsButton(string key, string? title = null, string? description = null, ButtonStyle style = ButtonStyle.Primary, bool isDisabled = false, Func<ValueTask>? callback = null)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        IsDisabled = isDisabled;
        Callback = callback ?? (() => ValueTask.CompletedTask);
    }

    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ButtonStyle Style { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual Func<ValueTask> Callback { get; set; } = () => ValueTask.CompletedTask;
}
