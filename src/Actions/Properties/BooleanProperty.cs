using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Actions.Properties;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Actions.Properties;

public class BooleanProperty : IBooleanProperty
{
    public BooleanProperty() { }

    [SetsRequiredMembers]
    public BooleanProperty(string parameter, string? title = null, string? description = null, BooleanSettingStyle style = BooleanSettingStyle.ToggleSwitch, bool defaultValue = false, bool isCompact = false, bool isDisabled = false)
    {
        Parameter = parameter;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
    }

    public virtual required string Parameter { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual BooleanSettingStyle Style { get; set; }

    public virtual bool DefaultValue { get; set; }

    public virtual bool IsCompact { get; set; }

    public bool IsDisabled { get; set; }
}

public static class BooleanPropertyExtensions
{
    public static void AddBooleanProperty(this FunctionAction action, string parameter, string? title = null, string? description = null, BooleanSettingStyle style = BooleanSettingStyle.ToggleSwitch, bool defaultValue = false, bool isCompact = false, bool isDisabled = false)
    {
        var property = new BooleanProperty(parameter, title, description, style, defaultValue, isCompact, isDisabled);
        action.Properties.Add(property);
    }
}
