using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Actions.Properties;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Actions.Properties;

public class DateTimeProperty : IDateTimeProperty
{
    public DateTimeProperty() { }

    [SetsRequiredMembers]
    public DateTimeProperty(string parameter, string? title = null, string? description = null, DateTimeSettingStyle style = DateTimeSettingStyle.DateTime, DateTimeOffset? defaultValue = null, DateTimeOffset? minValue = null, DateTimeOffset? maxValue = null, bool isDisabled = false)
    {
        Parameter = parameter;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue ?? DateTimeOffset.UtcNow;
        MinValue = minValue ?? DateTimeOffset.MinValue;
        MaxValue = maxValue ?? DateTimeOffset.MaxValue;
        IsDisabled = isDisabled;
    }

    public virtual required string Parameter { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual DateTimeSettingStyle Style { get; set; }

    public virtual DateTimeOffset DefaultValue { get; set; }

    public virtual DateTimeOffset MinValue { get; set; } = DateTimeOffset.MinValue;

    public virtual DateTimeOffset MaxValue { get; set; } = DateTimeOffset.MaxValue;

    public bool IsDisabled { get; set; }
}

public static class DateTimePropertyExtensions
{
    public static void AddDateTimeProperty(this FunctionAction action, string parameter, string? title = null, string? description = null, DateTimeSettingStyle style = DateTimeSettingStyle.DateTime, DateTimeOffset? defaultValue = null, DateTimeOffset? minValue = null, DateTimeOffset? maxValue = null, bool isDisabled = false)
    {
        var property = new DateTimeProperty(parameter, title, description, style, defaultValue, minValue, maxValue, isDisabled);
        action.Properties.Add(property);
    }
}
