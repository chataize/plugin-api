using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a date and/or time setting that can be displayed and configured in the user interface.
/// </summary>
public class DateTimeSetting : IDateTimeSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeSetting"/> class.
    /// </summary>
    public DateTimeSetting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeSetting"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">The optional description or helper text.</param>
    /// <param name="style">The style used to present the setting (e.g., DateTime, DateOnly, TimeOnly).</param>
    /// <param name="defaultValue">The default value of the setting. If null, current UTC time is used.</param>
    /// <param name="minValue">The minimum selectable value. Defaults to <see cref="DateTimeOffset.MinValue"/>.</param>
    /// <param name="maxValue">The maximum selectable value. Defaults to <see cref="DateTimeOffset.MaxValue"/>.</param>
    /// <param name="isDisabled">Indicates whether the setting is disabled and not editable.</param>
    [SetsRequiredMembers]
    public DateTimeSetting(
        string id,
        string? title = null,
        string? description = null,
        DateTimeSettingStyle style = DateTimeSettingStyle.DateTime,
        DateTimeOffset? defaultValue = null,
        DateTimeOffset? minValue = null,
        DateTimeOffset? maxValue = null,
        bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue ?? DateTimeOffset.UtcNow;
        MinValue = minValue ?? DateTimeOffset.MinValue;
        MaxValue = maxValue ?? DateTimeOffset.MaxValue;
        IsDisabled = isDisabled;
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual DateTimeSettingStyle Style { get; set; }

    /// <inheritdoc />
    public virtual DateTimeOffset DefaultValue { get; set; }

    /// <inheritdoc />
    public virtual object DefaultValueObject => DefaultValue;

    /// <inheritdoc />
    public virtual DateTimeOffset MinValue { get; set; } = DateTimeOffset.MinValue;

    /// <inheritdoc />
    public virtual DateTimeOffset MaxValue { get; set; } = DateTimeOffset.MaxValue;

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }
}

/// <summary>
/// Extension methods for adding <see cref="DateTimeSetting"/> instances to setting containers and collections.
/// </summary>
public static class DateTimeSettingExtensions
{
    /// <summary>
    /// Adds a <see cref="DateTimeSetting"/> to an editable settings container.
    /// </summary>
    /// <param name="container">The target container.</param>
    /// <param name="id">The setting's unique identifier.</param>
    /// <param name="title">Display title shown in the UI.</param>
    /// <param name="description">Optional helper text.</param>
    /// <param name="style">Style for the control (DateTime, DateOnly, TimeOnly).</param>
    /// <param name="defaultValue">The default value; if null, current UTC is used.</param>
    /// <param name="minValue">Minimum allowed value.</param>
    /// <param name="maxValue">Maximum allowed value.</param>
    /// <param name="isDisabled">Whether the setting is disabled.</param>
    /// <returns>The same container instance, for fluent chaining.</returns>
    public static IEditableSettingsContainer AddDateTimeSetting(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        DateTimeSettingStyle style = DateTimeSettingStyle.DateTime,
        DateTimeOffset? defaultValue = null,
        DateTimeOffset? minValue = null,
        DateTimeOffset? maxValue = null,
        bool isDisabled = false)
    {
        var setting = new DateTimeSetting(id, title, description, style, defaultValue, minValue, maxValue, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    /// <summary>
    /// Adds a <see cref="DateTimeSetting"/> to a settings collection.
    /// </summary>
    /// <param name="collection">The collection to which the setting is added.</param>
    /// <param name="id">The setting's unique identifier.</param>
    /// <param name="title">Display title shown in the UI.</param>
    /// <param name="description">Optional helper text.</param>
    /// <param name="style">Style for the control (DateTime, DateOnly, TimeOnly).</param>
    /// <param name="defaultValue">The default value; if null, current UTC is used.</param>
    /// <param name="minValue">Minimum allowed value.</param>
    /// <param name="maxValue">Maximum allowed value.</param>
    /// <param name="isDisabled">Whether the setting is disabled.</param>
    /// <returns>The same collection instance, for fluent chaining.</returns>
    public static ICollection<ISetting> AddDateTimeSetting(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        DateTimeSettingStyle style = DateTimeSettingStyle.DateTime,
        DateTimeOffset? defaultValue = null,
        DateTimeOffset? minValue = null,
        DateTimeOffset? maxValue = null,
        bool isDisabled = false)
    {
        var setting = new DateTimeSetting(id, title, description, style, defaultValue, minValue, maxValue, isDisabled);
        collection.Add(setting);

        return collection;
    }
}
