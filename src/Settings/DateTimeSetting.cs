using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

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
