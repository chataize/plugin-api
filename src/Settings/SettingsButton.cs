using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SettingsButton : ISettingsButton
{
    public SettingsButton() { }

    [SetsRequiredMembers]
    public SettingsButton(string key, string? title = null, string? description = null, ButtonStyle style = ButtonStyle.Primary, bool isDisabled = false, Func<CancellationToken, ValueTask>? callback = null)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        IsDisabled = isDisabled;
        Callback = callback;
    }

    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ButtonStyle Style { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual Func<CancellationToken, ValueTask>? Callback { get; set; }
}

public static class SettingsButtonExtensions
{
    public static void AddSettingsButton(this ChatbotPlugin plugin, string key, string? title = null, string? description = null, ButtonStyle style = ButtonStyle.Primary, bool isDisabled = false, Func<CancellationToken, ValueTask>? callback = null)
    {
        var setting = new SettingsButton(key, title, description, style, isDisabled, callback);
        plugin.Settings.Add(setting);
    }
}
