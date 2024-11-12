using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions;
using ChatAIze.Abstractions.Actions.Properties;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Actions.Properties;

public class SelectionProperty : ISelectionProperty
{
    public SelectionProperty() { }

    [SetsRequiredMembers]
    public SelectionProperty(string parameter, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, ICollection<ISelectionChoice>? choices = null)
    {
        Parameter = parameter;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
        Choices = choices ?? [];
    }

    [SetsRequiredMembers]
    public SelectionProperty(string parameter, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, params ISelectionChoice[] choices)
    {
        Parameter = parameter;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
        Choices = choices;
    }

    public virtual required string Parameter { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual SelectionSettingStyle Style { get; set; }

    public virtual string? DefaultValue { get; set; }

    public virtual bool IsCompact { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];
}

public static class SelectionPropertyExtensions
{
    public static void AddSelectionProperty(this FunctionAction action, string parameter, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, ICollection<ISelectionChoice>? choices = null)
    {
        var property = new SelectionProperty(parameter, title, description, style, defaultValue, isCompact, isDisabled, choices);
        action.Properties.Add(property);
    }

    public static void AddSelectionProperty(this FunctionAction action, string parameter, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false, params ISelectionChoice[] choices)
    {
        var property = new SelectionProperty(parameter, title, description, style, defaultValue, isCompact, isDisabled, choices);
        action.Properties.Add(property);
    }
}
