using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting<T> : ISelectionSetting<T>
{
    public SelectionSetting() { }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, T defaultValue, ICollection<ISelectionChoice<T>> choices)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, T defaultValue, params ISelectionChoice<T>[] choices)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, T defaultValue, ICollection<ISelectionChoice<T>> choices)
    {
        Key = key;
        Title = title;
        Description = description;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, T defaultValue, params ISelectionChoice<T>[] choices)
    {
        Key = key;
        Title = title;
        Description = description;
        Choices = choices;
        DefaultValue = defaultValue;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, SelectionSettingStyle style, T defaultValue, ICollection<ISelectionChoice<T>> choices)
    {
        Key = key;
        Title = title;
        Style = style;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, SelectionSettingStyle style, T defaultValue, params ISelectionChoice<T>[] choices)
    {
        Key = key;
        Title = title;
        Style = style;
        Choices = choices;
        DefaultValue = defaultValue;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, SelectionSettingStyle style, T defaultValue, ICollection<ISelectionChoice<T>> choices)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        Choices = choices;
    }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string title, string? description, SelectionSettingStyle style, T defaultValue, params ISelectionChoice<T>[] choices)
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

    public required virtual T DefaultValue { get; set; }

    public virtual ICollection<ISelectionChoice<T>> Choices { get; set; } = [];
}
