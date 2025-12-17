using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a decimal (floating-point) setting with support for slider presentation and value constraints.
/// </summary>
/// <remarks>
/// The host stores the user-provided value as JSON under <see cref="ISetting.Id"/>.
/// For plugin-level settings in ChatAIze.Chatbot, ids should be globally unique across all plugins.
/// </remarks>
public class DecimalSetting : IDecimalSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalSetting"/> class.
    /// </summary>
    public DecimalSetting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalSetting"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="defaultValue">The default decimal value for the setting.</param>
    /// <param name="minValue">The minimum allowable value.</param>
    /// <param name="maxValue">The maximum allowable value.</param>
    /// <param name="showSliderValue">Indicates whether the current slider value should be displayed.</param>
    /// <param name="showSliderPercentage">Indicates whether the slider should show percentage values.</param>
    /// <param name="minValueLabel">Label to show when the slider is at its minimum value (or null to use the value itself).</param>
    /// <param name="maxValueLabel">Label to show when the slider is at its maximum value (or null to use the value itself).</param>
    /// <param name="isDisabled">Indicates whether the setting is disabled and not editable.</param>
    [SetsRequiredMembers]
    public DecimalSetting(
        string id,
        string? title = null,
        string? description = null,
        double defaultValue = 0.0,
        double minValue = double.MinValue,
        double maxValue = double.MaxValue,
        bool showSliderValue = true,
        bool showSliderPercentage = false,
        string? minValueLabel = null,
        string? maxValueLabel = null,
        bool isDisabled = false)
    {
        Id = id;
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

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual double DefaultValue { get; set; }

    /// <inheritdoc />
    public virtual object DefaultValueObject => DefaultValue;

    /// <inheritdoc />
    public virtual double MinValue { get; set; } = double.MinValue;

    /// <inheritdoc />
    public virtual double MaxValue { get; set; } = double.MaxValue;

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
/// Extension methods for adding <see cref="DecimalSetting"/> instances to containers or collections.
/// </summary>
public static class DecimalSettingExtensions
{
    /// <summary>
    /// Adds a <see cref="DecimalSetting"/> to an editable settings container.
    /// </summary>
    /// <param name="container">The container to which the setting will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">Display title of the setting.</param>
    /// <param name="description">Optional helper text.</param>
    /// <param name="defaultValue">Default value of the setting.</param>
    /// <param name="minValue">Minimum allowable value.</param>
    /// <param name="maxValue">Maximum allowable value.</param>
    /// <param name="showSliderValue">Whether to show the current slider value.</param>
    /// <param name="showSliderPercentage">Whether to show the slider as a percentage.</param>
    /// <param name="minValueLabel">Optional label for the minimum value.</param>
    /// <param name="maxValueLabel">Optional label for the maximum value.</param>
    /// <param name="isDisabled">Indicates if the setting is disabled.</param>
    /// <returns>The modified settings container.</returns>
    public static IEditableSettingsContainer AddDecimalSetting(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        double defaultValue = 0.0,
        double minValue = double.MinValue,
        double maxValue = double.MaxValue,
        bool showSliderValue = true,
        bool showSliderPercentage = false,
        string? minValueLabel = null,
        string? maxValueLabel = null,
        bool isDisabled = false)
    {
        var setting = new DecimalSetting(id, title, description, defaultValue, minValue, maxValue, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        container.Settings.Add(setting);
        
        return container;
    }

    /// <summary>
    /// Adds a <see cref="DecimalSetting"/> to a settings collection.
    /// </summary>
    /// <param name="collection">The collection to which the setting will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">Display title of the setting.</param>
    /// <param name="description">Optional helper text.</param>
    /// <param name="defaultValue">Default value of the setting.</param>
    /// <param name="minValue">Minimum allowable value.</param>
    /// <param name="maxValue">Maximum allowable value.</param>
    /// <param name="showSliderValue">Whether to show the current slider value.</param>
    /// <param name="showSliderPercentage">Whether to show the slider as a percentage.</param>
    /// <param name="minValueLabel">Optional label for the minimum value.</param>
    /// <param name="maxValueLabel">Optional label for the maximum value.</param>
    /// <param name="isDisabled">Indicates if the setting is disabled.</param>
    /// <returns>The modified collection of settings.</returns>
    public static ICollection<ISetting> AddDecimalSetting(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        double defaultValue = 0.0,
        double minValue = double.MinValue,
        double maxValue = double.MaxValue,
        bool showSliderValue = true,
        bool showSliderPercentage = false,
        string? minValueLabel = null,
        string? maxValueLabel = null,
        bool isDisabled = false)
    {
        var setting = new DecimalSetting(id, title, description, defaultValue, minValue, maxValue, showSliderValue, showSliderPercentage, minValueLabel, maxValueLabel, isDisabled);
        collection.Add(setting);

        return collection;
    }
}
