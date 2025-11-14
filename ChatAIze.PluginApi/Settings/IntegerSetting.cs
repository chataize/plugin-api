using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents an integer-based setting with optional range limits, step size, and slider display options.
/// </summary>
public class IntegerSetting : IIntegerSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerSetting"/> class.
    /// </summary>
    public IntegerSetting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerSetting"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="style">The style used to present the setting (e.g., stepper, slider).</param>
    /// <param name="defaultValue">The default integer value for the setting.</param>
    /// <param name="minValue">The minimum allowable value.</param>
    /// <param name="maxValue">The maximum allowable value.</param>
    /// <param name="step">The increment/decrement step size.</param>
    /// <param name="showSliderValue">Indicates whether the current slider value should be displayed.</param>
    /// <param name="showSliderPercentage">Indicates whether the slider should show percentage values.</param>
    /// <param name="minValueLabel">Label to show when the slider is at its minimum value (or null to use the value itself).</param>
    /// <param name="maxValueLabel">Label to show when the slider is at its maximum value (or null to use the value itself).</param>
    /// <param name="isDisabled">Indicates whether the setting is disabled and not editable.</param>
    [SetsRequiredMembers]
    public IntegerSetting(
        string id,
        string? title = null,
        string? description = null,
        IntegerSettingStyle style = IntegerSettingStyle.Stepper,
        int defaultValue = 0,
        int minValue = int.MinValue,
        int maxValue = int.MaxValue,
        int step = 1,
        bool showSliderValue = true,
        bool showSliderPercentage = false,
        string? minValueLabel = null,
        string? maxValueLabel = null,
        bool isDisabled = false)
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

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual IntegerSettingStyle Style { get; set; }

    /// <inheritdoc />
    public virtual int DefaultValue { get; set; }

    /// <inheritdoc />
    public virtual object DefaultValueObject => DefaultValue;

    /// <inheritdoc />
    public virtual int MinValue { get; set; } = int.MinValue;

    /// <inheritdoc />
    public virtual int MaxValue { get; set; } = int.MaxValue;

    /// <inheritdoc />
    public virtual int Step { get; set; } = 1;

    /// <inheritdoc />
    public virtual bool ShowSliderValue { get; set; } = true;

    /// <inheritdoc />
    public virtual bool ShowSliderPercentage { get; set; }

    /// <inheritdoc />
    public virtual string? MinValueLabel { get; set; }

    /// <inheritdoc />
    public virtual string? MaxValueLabel { get; set; }

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }
}

/// <summary>
/// Extension methods for adding <see cref="IntegerSetting"/> instances to setting containers and collections.
/// </summary>
public static class IntegerSettingExtensions
{
    /// <summary>
    /// Adds an <see cref="IntegerSetting"/> to an editable settings container.
    /// </summary>
    public static IEditableSettingsContainer AddIntegerSetting(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        IntegerSettingStyle style = IntegerSettingStyle.Stepper,
        int defaultValue = 0,
        int minValue = int.MinValue,
        int maxValue = int.MaxValue,
        int step = 1,
        bool showSliderValue = true,
        bool showSliderPercentage = false,
        string? minValueLabel = null,
        string? maxValueLabel = null,
        bool isDisabled = false)
    {
        var setting = new IntegerSetting(id, title, description, style, defaultValue, minValue, maxValue, step, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    /// <summary>
    /// Adds an <see cref="IntegerSetting"/> to a settings collection.
    /// </summary>
    public static ICollection<ISetting> AddIntegerSetting(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        IntegerSettingStyle style = IntegerSettingStyle.Stepper,
        int defaultValue = 0,
        int minValue = int.MinValue,
        int maxValue = int.MaxValue,
        int step = 1,
        bool showSliderValue = true,
        bool showSliderPercentage = false,
        string? minValueLabel = null,
        string? maxValueLabel = null,
        bool isDisabled = false)
    {
        var setting = new IntegerSetting(id, title, description, style, defaultValue, minValue, maxValue, step, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        collection.Add(setting);
        
        return collection;
    }
}
