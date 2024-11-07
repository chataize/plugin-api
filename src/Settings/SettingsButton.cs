using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SettingsButton : ISettingsButton
{
    public SettingsButton() { }

    [SetsRequiredMembers]
    public SettingsButton(string title, Func<ValueTask>? callback = null)
    {
        Title = title;
        Callback = callback ?? (() => ValueTask.CompletedTask);
    }

    [SetsRequiredMembers]
    public SettingsButton(string title, ButtonStyle style, Func<ValueTask>? callback = null)
    {
        Title = title;
        Style = style;
        Callback = callback ?? (() => ValueTask.CompletedTask);
    }

    [SetsRequiredMembers]
    public SettingsButton(string title, string? description, ButtonStyle style = ButtonStyle.Default, Func<ValueTask>? callback = null)
    {
        Title = title;
        Description = description;
        Style = style;
        Callback = callback ?? (() => ValueTask.CompletedTask);
    }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ButtonStyle Style { get; set; }

    public virtual Func<ValueTask> Callback { get; set; } = () => ValueTask.CompletedTask;
}
