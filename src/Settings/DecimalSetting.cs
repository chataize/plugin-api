using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class DecimalSetting : IDecimalSetting
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual double DefaultValue { get; set; }

    public virtual double MinValue { get; set; } = double.MinValue;

    public virtual double MaxValue { get; set; } = double.MaxValue;

    public virtual bool ShowSliderValue { get; set; }

    public virtual bool ShowSliderPercentage { get; set; }

    public virtual string? MinValueLabel { get; set; }

    public virtual string? MaxValueLabel { get; set; }

    public virtual bool IsDisabled { get; set; }
}
