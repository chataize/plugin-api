using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a boolean (true/false) setting that can be displayed and configured in the user interface.
/// </summary>
/// <remarks>
/// The host stores the user-provided value as JSON under <see cref="ISetting.Id"/>.
/// For plugin-level settings in ChatAIze.Chatbot, ids should be globally unique across all plugins.
/// </remarks>
public class BooleanSetting : IBooleanSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanSetting"/> class.
    /// </summary>
    public BooleanSetting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanSetting"/> class with predefined values.
    /// </summary>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">The description or helper text for the setting.</param>
    /// <param name="style">The visual style used to render the setting (e.g., toggle, checkbox).</param>
    /// <param name="defaultValue">The default value of the setting.</param>
    /// <param name="isCompact">If true, renders the setting without a label.</param>
    /// <param name="isDisabled">If true, the setting is disabled and cannot be modified by the user.</param>
    [SetsRequiredMembers]
    public BooleanSetting(
        string id,
        string? title = null,
        string? description = null,
        BooleanSettingStyle style = BooleanSettingStyle.ToggleSwitch,
        bool defaultValue = false,
        bool isCompact = false,
        bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual BooleanSettingStyle Style { get; set; }

    /// <inheritdoc />
    public virtual bool DefaultValue { get; set; }

    /// <inheritdoc />
    public virtual object DefaultValueObject => DefaultValue;

    /// <inheritdoc />
    public virtual bool IsCompact { get; set; }

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }
}

/// <summary>
/// Extension methods for adding <see cref="BooleanSetting"/> instances to setting containers and collections.
/// </summary>
public static class BooleanSettingExtensions
{
    /// <summary>
    /// Adds a new <see cref="BooleanSetting"/> to the editable settings container.
    /// </summary>
    /// <param name="container">The settings container to which the setting will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">The description or helper text for the setting.</param>
    /// <param name="style">The visual style used to render the setting.</param>
    /// <param name="defaultValue">The default value of the setting.</param>
    /// <param name="isCompact">If true, renders the setting without a label.</param>
    /// <param name="isDisabled">If true, the setting is disabled and cannot be modified.</param>
    /// <returns>The modified container, allowing for fluent chaining.</returns>
    public static IEditableSettingsContainer AddBooleanSetting(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        BooleanSettingStyle style = BooleanSettingStyle.ToggleSwitch,
        bool defaultValue = false,
        bool isCompact = false,
        bool isDisabled = false)
    {
        var setting = new BooleanSetting(id, title, description, style, defaultValue, isCompact, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    /// <summary>
    /// Adds a new <see cref="BooleanSetting"/> to the collection of settings.
    /// </summary>
    /// <param name="collection">The collection to which the setting will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">The description or helper text for the setting.</param>
    /// <param name="style">The visual style used to render the setting.</param>
    /// <param name="defaultValue">The default value of the setting.</param>
    /// <param name="isCompact">If true, renders the setting without a label.</param>
    /// <param name="isDisabled">If true, the setting is disabled and cannot be modified.</param>
    /// <returns>The modified collection, allowing for fluent chaining.</returns>
    public static ICollection<ISetting> AddBooleanSetting(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        BooleanSettingStyle style = BooleanSettingStyle.ToggleSwitch,
        bool defaultValue = false,
        bool isCompact = false,
        bool isDisabled = false)
    {
        var setting = new BooleanSetting(id, title, description, style, defaultValue, isCompact, isDisabled);
        collection.Add(setting);

        return collection;
    }
}
