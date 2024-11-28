using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SettingsButton : ISettingsButton
{
    public SettingsButton() { }

    [SetsRequiredMembers]
    public SettingsButton(string id, Func<CancellationToken, ValueTask> callback, string? title = null, string? description = null, ButtonStyle style = ButtonStyle.Primary, bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        IsDisabled = isDisabled;
        Callback = callback;
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ButtonStyle Style { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual required Func<CancellationToken, ValueTask> Callback { get; set; }
}

public static class SettingsButtonExtensions
{
    public static IEditableSettingsContainer AddSettingsButton(this IEditableSettingsContainer container, string id, Func<CancellationToken, ValueTask> callback, string? title = null, string? description = null, ButtonStyle style = ButtonStyle.Primary, bool isDisabled = false)
    {
        var setting = new SettingsButton(id, callback, title, description, style, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    public static ICollection<ISetting> AddSettingsButton(this ICollection<ISetting> collction, string id, Func<CancellationToken, ValueTask> callback, string? title = null, string? description = null, ButtonStyle style = ButtonStyle.Primary, bool isDisabled = false)
    {
        var setting = new SettingsButton(id, callback, title, description, style, isDisabled);
        collction.Add(setting);

        return collction;
    }
}
