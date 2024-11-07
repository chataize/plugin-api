using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting : ISelectionSetting
{
    public SelectionSetting() { }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string defaultValue, ICollection<ISelectionChoice> choices)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string defaultValue, params ISelectionChoice[] choices)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, string defaultValue, ICollection<ISelectionChoice> choices)
    {
        Key = key;
        Title = title;
        Description = description;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, string defaultValue, params ISelectionChoice[] choices)
    {
        Key = key;
        Title = title;
        Description = description;
        Choices = choices;
        DefaultValue = defaultValue;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, SelectionSettingStyle style, string defaultValue, ICollection<ISelectionChoice> choices)
    {
        Key = key;
        Title = title;
        Style = style;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, SelectionSettingStyle style, string defaultValue, params ISelectionChoice[] choices)
    {
        Key = key;
        Title = title;
        Style = style;
        Choices = choices;
        DefaultValue = defaultValue;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, SelectionSettingStyle style, string defaultValue, ICollection<ISelectionChoice> choices)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, SelectionSettingStyle style, string defaultValue, params ISelectionChoice[] choices)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual SelectionSettingStyle Style { get; set; }

    public required virtual string DefaultValue { get; set; }

    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];

    SelectionSettingStyle ISelectionSetting.Style => throw new NotImplementedException();
}
