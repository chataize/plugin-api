using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class DecimalSetting : IDecimalSetting
{
    public DecimalSetting() { }

    [SetsRequiredMembers]
    public DecimalSetting(string key, string title, double defaultValue = 0.0, double minValue = double.MinValue, double maxValue = double.MaxValue)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    [SetsRequiredMembers]
    public DecimalSetting(string key, string title, string? description, double defaultValue = 0.0, double minValue = double.MinValue, double maxValue = double.MaxValue)
    {
        Key = key;
        Title = title;
        Description = description;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual double DefaultValue { get; set; }

    public virtual double MinValue { get; set; } = double.MinValue;

    public virtual double MaxValue { get; set; } = double.MaxValue;
}
