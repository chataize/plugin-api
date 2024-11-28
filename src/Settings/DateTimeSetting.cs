using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class DateTimeSetting : IDateTimeSetting
{
    public DateTimeSetting() { }

    [SetsRequiredMembers]
    public DateTimeSetting(string id, string? title = null, string? description = null, DateTimeSettingStyle style = DateTimeSettingStyle.DateTime, DateTimeOffset? defaultValue = null, DateTimeOffset? minValue = null, DateTimeOffset? maxValue = null, bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue ?? DateTimeOffset.UtcNow;
        MinValue = minValue ?? DateTimeOffset.MinValue;
        MaxValue = maxValue ?? DateTimeOffset.MaxValue;
        IsDisabled = isDisabled;
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual DateTimeSettingStyle Style { get; set; }

    public virtual DateTimeOffset DefaultValue { get; set; }

    public virtual object DefaultValueObject => DefaultValue;

    public virtual DateTimeOffset MinValue { get; set; } = DateTimeOffset.MinValue;

    public virtual DateTimeOffset MaxValue { get; set; } = DateTimeOffset.MaxValue;

    public virtual bool IsDisabled { get; set; }
}

public static class DateTimeSettingExtensions
{
    public static IEditableSettingsContainer AddDateTimeSetting(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, DateTimeSettingStyle style = DateTimeSettingStyle.DateTime, DateTimeOffset? defaultValue = null, DateTimeOffset? minValue = null, DateTimeOffset? maxValue = null, bool isDisabled = false)
    {
        var setting = new DateTimeSetting(id, title, description, style, defaultValue, minValue, maxValue, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    public static ICollection<ISetting> AddDateTimeSetting(this ICollection<ISetting> collection, string id, string? title = null, string? description = null, DateTimeSettingStyle style = DateTimeSettingStyle.DateTime, DateTimeOffset? defaultValue = null, DateTimeOffset? minValue = null, DateTimeOffset? maxValue = null, bool isDisabled = false)
    {
        var setting = new DateTimeSetting(id, title, description, style, defaultValue, minValue, maxValue, isDisabled);
        collection.Add(setting);

        return collection;
    }
}
