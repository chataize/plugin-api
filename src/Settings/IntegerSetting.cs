using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class IntegerSetting : IIntegerSetting
{
    public IntegerSetting() { }

    [SetsRequiredMembers]
    public IntegerSetting(string key, string title, int defaultValue = 0, int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    [SetsRequiredMembers]
    public IntegerSetting(string key, string title, IntegerSettingStyle style, int defaultValue = 0, int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        Key = key;
        Title = title;
        Style = style;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    [SetsRequiredMembers]
    public IntegerSetting(string key, string title, string? description, IntegerSettingStyle style = IntegerSettingStyle.Slider, int defaultValue = 0, int minValue = int.MinValue, int maxValue = int.MaxValue)
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

    public virtual IntegerSettingStyle Style { get; set; }

    public virtual int DefaultValue { get; set; }

    public virtual int MinValue { get; set; } = int.MinValue;

    public virtual int MaxValue { get; set; } = int.MaxValue;
}
