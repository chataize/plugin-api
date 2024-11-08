using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class DateTimeSetting : IDateTimeSetting
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual DateTimeSettingStyle Style { get; set; }

    public virtual DateTimeOffset DefaultValue { get; set; }

    public virtual DateTimeOffset MinValue { get; set; } = DateTimeOffset.MinValue;

    public virtual DateTimeOffset MaxValue { get; set; } = DateTimeOffset.MaxValue;

    public bool IsDisabled { get; set; }
}
