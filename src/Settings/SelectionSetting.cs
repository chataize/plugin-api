using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting : ISelectionSetting
{
    public SelectionSetting() { }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, ICollection<ISelectionChoice>? choices = null)
    {
        Key = key;
        Title = title;
        Choices = choices ?? [];
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, params ISelectionChoice[] choices)
    {
        Key = key;
        Title = title;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, SelectionSettingStyle style, ICollection<ISelectionChoice>? choices = null)
    {
        Key = key;
        Title = title;
        Style = style;
        Choices = choices ?? [];
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, SelectionSettingStyle style, params ISelectionChoice[] choices)
    {
        Key = key;
        Title = title;
        Style = style;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, SelectionSettingStyle style = SelectionSettingStyle.Automatic, ICollection<ISelectionChoice>? choices = null)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        Choices = choices ?? [];
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, SelectionSettingStyle style = SelectionSettingStyle.Automatic, params ISelectionChoice[] choices)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        Choices = choices;
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual SelectionSettingStyle Style { get; set; }

    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];
}
