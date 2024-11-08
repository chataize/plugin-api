using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class IntegerSetting : IIntegerSetting
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual IntegerSettingStyle Style { get; set; }

    public virtual int DefaultValue { get; set; }

    public virtual int MinValue { get; set; } = int.MinValue;

    public virtual int MaxValue { get; set; } = int.MaxValue;

    public virtual int Step { get; set; } = 1;

    public virtual bool ShowSliderValue { get; set; }

    public virtual bool ShowSliderPercentage { get; set; }

    public virtual string? MinValueLabel { get; set; }

    public virtual string? MaxValueLabel { get; set; }

    public virtual bool IsDisabled { get; set; }
}
