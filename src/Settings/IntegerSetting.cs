using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class IntegerSetting : IIntegerSetting
{
    public IntegerSetting() { }

    [SetsRequiredMembers]
    public IntegerSetting(string id, string? title = null, string? description = null, IntegerSettingStyle style = IntegerSettingStyle.Stepper, int defaultValue = 0, int minValue = int.MinValue, int maxValue = int.MaxValue, int step = 1, bool showSliderValue = true, bool showSliderPercentage = false, string? minValueLabel = null, string? maxValueLabel = null, bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        MinValue = minValue;
        MaxValue = maxValue;
        Step = step;
        ShowSliderValue = showSliderValue;
        ShowSliderPercentage = showSliderPercentage;
        MinValueLabel = minValueLabel;
        MaxValueLabel = maxValueLabel;
        IsDisabled = isDisabled;
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual IntegerSettingStyle Style { get; set; }

    public virtual int DefaultValue { get; set; }

    public virtual int MinValue { get; set; } = int.MinValue;

    public virtual int MaxValue { get; set; } = int.MaxValue;

    public virtual int Step { get; set; } = 1;

    public virtual bool ShowSliderValue { get; set; } = true;

    public virtual bool ShowSliderPercentage { get; set; }

    public virtual string? MinValueLabel { get; set; }

    public virtual string? MaxValueLabel { get; set; }

    public virtual bool IsDisabled { get; set; }
}

public static class IntegerSettingExtensions
{
    public static void AddIntegerSetting(this ChatbotPlugin plugin, string id, string? title = null, string? description = null, IntegerSettingStyle style = IntegerSettingStyle.Stepper, int defaultValue = 0, int minValue = int.MinValue, int maxValue = int.MaxValue, int step = 1, bool showSliderValue = true, bool showSliderPercentage = false, string? minValueLabel = null, string? maxValueLabel = null, bool isDisabled = false)
    {
        var setting = new IntegerSetting(id, title, description, style, defaultValue, minValue, maxValue, step, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        plugin.Settings.Add(setting);
    }

    public static void AddIntegerSetting(this FunctionAction action, string id, string? title = null, string? description = null, IntegerSettingStyle style = IntegerSettingStyle.Stepper, int defaultValue = 0, int minValue = int.MinValue, int maxValue = int.MaxValue, int step = 1, bool showSliderValue = true, bool showSliderPercentage = false, string? minValueLabel = null, string? maxValueLabel = null, bool isDisabled = false)
    {
        var setting = new IntegerSetting(id, title, description, style, defaultValue, minValue, maxValue, step, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        action.Settings.Add(setting);
    }

    public static void AddIntegerSetting(this FunctionCondition condition, string id, string? title = null, string? description = null, IntegerSettingStyle style = IntegerSettingStyle.Stepper, int defaultValue = 0, int minValue = int.MinValue, int maxValue = int.MaxValue, int step = 1, bool showSliderValue = true, bool showSliderPercentage = false, string? minValueLabel = null, string? maxValueLabel = null, bool isDisabled = false)
    {
        var setting = new IntegerSetting(id, title, description, style, defaultValue, minValue, maxValue, step, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        condition.Settings.Add(setting);
    }
}
