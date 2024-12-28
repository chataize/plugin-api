using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting : ISelectionSetting
{
    public SelectionSetting() { }

    [SetsRequiredMembers]
    public SelectionSetting(string id, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, params ICollection<ISelectionChoice>? choices)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
        Choices = choices ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual SelectionSettingStyle Style { get; set; }

    public virtual string? DefaultValue { get; set; }

    public virtual object DefaultValueObject => DefaultValue ?? Choices.FirstOrDefault()?.Value ?? string.Empty;

    public virtual bool IsCompact { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];

    IReadOnlyCollection<ISelectionChoice> ISelectionSetting.Choices => (IReadOnlyCollection<ISelectionChoice>)Choices;
}

public static class SelectionSettingExtensions
{
    public static IEditableSettingsContainer AddSelectionSetting(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, params ICollection<ISelectionChoice>? choices)
    {
        var setting = new SelectionSetting(id, title, description, style, defaultValue, isCompact, isDisabled, choices);
        container.Settings.Add(setting);

        return container;
    }

    public static ICollection<ISetting> AddSelectionSetting(this ICollection<ISetting> collection, string id, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, params ICollection<ISelectionChoice>? choices)
    {
        var setting = new SelectionSetting(id, title, description, style, defaultValue, isCompact, isDisabled, choices);
        collection.Add(setting);

        return collection;
    }
}
