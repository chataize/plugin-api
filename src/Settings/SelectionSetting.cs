using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting : ISelectionSetting
{
    public SelectionSetting() { }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, ICollection<ISelectionChoice>? choices = null)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
        Choices = choices ?? [];
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, params ISelectionChoice[] choices)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
        Choices = choices;
    }

    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual SelectionSettingStyle Style { get; set; }

    public virtual string? DefaultValue { get; set; }

    public virtual bool IsCompact { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];
}

public static class SelectionSettingExtensions
{
    public static void AddSelectionSetting(this ChatbotPlugin plugin, string key, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, ICollection<ISelectionChoice>? choices = null)
    {
        var setting = new SelectionSetting(key, title, description, style, defaultValue, isCompact, isDisabled, choices);
        plugin.Settings.Add(setting);
    }

    public static void AddSelectionSetting(this ChatbotPlugin plugin, string key, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, params ISelectionChoice[] choices)
    {
        var setting = new SelectionSetting(key, title, description, style, defaultValue, isCompact, isDisabled, choices);
        plugin.Settings.Add(setting);
    }
}
