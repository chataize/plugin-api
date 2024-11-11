using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Actions;

namespace ChatAIze.PluginApi.Settings;

public class DecimalSetting : IDecimalSetting
{
    public DecimalSetting() { }

    [SetsRequiredMembers]
    public DecimalSetting(string key, string? title = null, string? description = null, double defaultValue = 0.0, double minValue = double.MinValue, double maxValue = double.MaxValue, bool showSliderValue = true, bool showSliderPercentage = false, string? minValueLabel = null, string? maxValueLabel = null, bool isDisabled = false)
    {
        Key = key;
        Title = title;
        Description = description;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
        ShowSliderValue = showSliderValue;
        ShowSliderPercentage = showSliderPercentage;
        MinValueLabel = minValueLabel;
        MaxValueLabel = maxValueLabel;
        IsDisabled = isDisabled;
    }

    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual double DefaultValue { get; set; }

    public virtual double MinValue { get; set; } = double.MinValue;

    public virtual double MaxValue { get; set; } = double.MaxValue;

    public virtual bool ShowSliderValue { get; set; } = true;

    public virtual bool ShowSliderPercentage { get; set; }

    public virtual string? MinValueLabel { get; set; }

    public virtual string? MaxValueLabel { get; set; }

    public virtual bool IsDisabled { get; set; }
}

public static class DecimalSettingExtensions
{
    public static void AddDecimalSetting(this ChatbotPlugin plugin, string key, string? title = null, string? description = null, double defaultValue = 0.0, double minValue = double.MinValue, double maxValue = double.MaxValue, bool showSliderValue = true, bool showSliderPercentage = false, string? minValueLabel = null, string? maxValueLabel = null, bool isDisabled = false)
    {
        var setting = new DecimalSetting(key, title, description, defaultValue, minValue, maxValue, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        plugin.Settings.Add(setting);
    }

    public static void AddDecimalSetting(this FunctionAction action, string key, string? title = null, string? description = null, double defaultValue = 0.0, double minValue = double.MinValue, double maxValue = double.MaxValue, bool showSliderValue = true, bool showSliderPercentage = false, string? minValueLabel = null, string? maxValueLabel = null, bool isDisabled = false)
    {
        var setting = new DecimalSetting(key, title, description, defaultValue, minValue, maxValue, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        action.Settings.Add(setting);
    }
}
