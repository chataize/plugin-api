using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class DateTimeSetting : IDateTimeSetting
{
    public DateTimeSetting() { }

    [SetsRequiredMembers]
    public DateTimeSetting(string key, string title, DateTimeOffset defaultValue)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
    }

    [SetsRequiredMembers]
    public DateTimeSetting(string key, string title, string? description, DateTimeOffset defaultValue)
    {
        Key = key;
        Title = title;
        Description = description;
        DefaultValue = defaultValue;
    }

    [SetsRequiredMembers]
    public DateTimeSetting(string key, string title, DateTimeOffset defaultValue, DateTimeOffset minValue, DateTimeOffset maxValue)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    [SetsRequiredMembers]
    public DateTimeSetting(string key, string title, string? description, DateTimeOffset defaultValue, DateTimeOffset minValue, DateTimeOffset maxValue)
    {
        Key = key;
        Title = title;
        Description = description;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    [SetsRequiredMembers]
    public DateTimeSetting(string key, string title, DateTimeSettingStyle style, DateTimeOffset defaultValue, DateTimeOffset minValue, DateTimeOffset maxValue)
    {
        Key = key;
        Title = title;
        Style = style;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    [SetsRequiredMembers]
    public DateTimeSetting(string key, string title, string? description, DateTimeSettingStyle style, DateTimeOffset defaultValue, DateTimeOffset minValue, DateTimeOffset maxValue)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual DateTimeSettingStyle Style { get; set; }

    public virtual DateTimeOffset DefaultValue { get; set; }

    public virtual DateTimeOffset MinValue { get; set; } = DateTimeOffset.MinValue;

    public virtual DateTimeOffset MaxValue { get; set; } = DateTimeOffset.MaxValue;

    DateTimeSettingStyle IDateTimeSetting.Style => throw new NotImplementedException();
}
