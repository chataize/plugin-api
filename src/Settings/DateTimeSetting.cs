using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;
using ChatAIze.PluginApi.Actions;

namespace ChatAIze.PluginApi.Settings;

public class DateTimeSetting : IDateTimeSetting
{
    public DateTimeSetting() { }

    [SetsRequiredMembers]
    public DateTimeSetting(string key, string? title = null, string? description = null, DateTimeSettingStyle style = DateTimeSettingStyle.DateTime, DateTimeOffset? defaultValue = null, DateTimeOffset? minValue = null, DateTimeOffset? maxValue = null, bool isDisabled = false)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue ?? DateTimeOffset.UtcNow;
        MinValue = minValue ?? DateTimeOffset.MinValue;
        MaxValue = maxValue ?? DateTimeOffset.MaxValue;
        IsDisabled = isDisabled;
    }

    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual DateTimeSettingStyle Style { get; set; }

    public virtual DateTimeOffset DefaultValue { get; set; }

    public virtual DateTimeOffset MinValue { get; set; } = DateTimeOffset.MinValue;

    public virtual DateTimeOffset MaxValue { get; set; } = DateTimeOffset.MaxValue;

    public bool IsDisabled { get; set; }
}

public static class DateTimeSettingExtensions
{
    public static void AddDateTimeSetting(this ChatbotPlugin plugin, string key, string? title = null, string? description = null, DateTimeSettingStyle style = DateTimeSettingStyle.DateTime, DateTimeOffset? defaultValue = null, DateTimeOffset? minValue = null, DateTimeOffset? maxValue = null, bool isDisabled = false)
    {
        var setting = new DateTimeSetting(key, title, description, style, defaultValue, minValue, maxValue, isDisabled);
        plugin.Settings.Add(setting);
    }

    public static void AddDateTimeSetting(this FunctionAction action, string key, string? title = null, string? description = null, DateTimeSettingStyle style = DateTimeSettingStyle.DateTime, DateTimeOffset? defaultValue = null, DateTimeOffset? minValue = null, DateTimeOffset? maxValue = null, bool isDisabled = false)
    {
        var setting = new DateTimeSetting(key, title, description, style, defaultValue, minValue, maxValue, isDisabled);
        action.Settings.Add(setting);
    }
}
