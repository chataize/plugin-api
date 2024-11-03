using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class DecimalSetting : IDecimalSetting
{
    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual double DefaultValue { get; set; }

    public virtual double MinValue { get; set; }

    public virtual double MaxValue { get; set; }
}
