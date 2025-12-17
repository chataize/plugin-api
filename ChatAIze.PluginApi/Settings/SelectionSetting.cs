using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a selection setting where the user chooses one option from a predefined list of choices.
/// </summary>
/// <remarks>
/// <para>
/// The host stores the selected <see cref="ISelectionChoice.Value"/> as JSON under <see cref="ISetting.Id"/>.
/// For plugin-level settings in ChatAIze.Chatbot, ids should be globally unique across all plugins.
/// </para>
/// <para>
/// Host support for per-choice disabling (<see cref="ISelectionChoice.IsDisabled"/>) can vary.
/// </para>
/// </remarks>
public class SelectionSetting : ISelectionSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionSetting"/> class.
    /// </summary>
    public SelectionSetting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionSetting"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="style">The visual style used to render the choices.</param>
    /// <param name="defaultValue">The default selected value. If null, the first available choice is used as fallback.</param>
    /// <param name="isCompact">Indicates whether the setting should be rendered without a label or additional spacing.</param>
    /// <param name="isDisabled">Indicates whether the setting is disabled and not editable.</param>
    /// <param name="choices">The available options to choose from.</param>
    [SetsRequiredMembers]
    public SelectionSetting(
        string id,
        string? title = null,
        string? description = null,
        SelectionSettingStyle style = SelectionSettingStyle.Automatic,
        string? defaultValue = null,
        bool isCompact = false,
        bool isDisabled = false,
        params ICollection<ISelectionChoice>? choices)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
        Choices = choices ?? [];
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual SelectionSettingStyle Style { get; set; }

    /// <inheritdoc />
    public virtual string? DefaultValue { get; set; }

    /// <inheritdoc />
    public virtual object DefaultValueObject => DefaultValue ?? Choices.FirstOrDefault()?.Value ?? string.Empty;

    /// <inheritdoc />
    public virtual bool IsCompact { get; set; }

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }

    /// <summary>
    /// Gets or sets the collection of available selection choices.
    /// </summary>
    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<ISelectionChoice> ISelectionSetting.Choices => (IReadOnlyCollection<ISelectionChoice>)Choices;
}

/// <summary>
/// Extension methods for adding <see cref="SelectionSetting"/> instances to setting containers and collections.
/// </summary>
public static class SelectionSettingExtensions
{
    /// <summary>
    /// Adds a <see cref="SelectionSetting"/> to an editable settings container.
    /// </summary>
    /// <param name="container">The container to which the setting will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The title of the setting.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="style">The visual style used to render the choices.</param>
    /// <param name="defaultValue">The default selected value.</param>
    /// <param name="isCompact">Whether to render the setting in a compact form.</param>
    /// <param name="isDisabled">Whether the setting is disabled.</param>
    /// <param name="choices">The available options to choose from.</param>
    /// <returns>The same container instance, allowing method chaining.</returns>
    public static IEditableSettingsContainer AddSelectionSetting(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        SelectionSettingStyle style = SelectionSettingStyle.Automatic,
        string? defaultValue = null,
        bool isCompact = false,
        bool isDisabled = false,
        params ICollection<ISelectionChoice>? choices)
    {
        var setting = new SelectionSetting(id, title, description, style, defaultValue, isCompact, isDisabled, choices);
        container.Settings.Add(setting);
        
        return container;
    }

    /// <summary>
    /// Adds a <see cref="SelectionSetting"/> to a settings collection.
    /// </summary>
    /// <param name="collection">The collection to which the setting will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The title of the setting.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="style">The visual style used to render the choices.</param>
    /// <param name="defaultValue">The default selected value.</param>
    /// <param name="isCompact">Whether to render the setting in a compact form.</param>
    /// <param name="isDisabled">Whether the setting is disabled.</param>
    /// <param name="choices">The available options to choose from.</param>
    /// <returns>The same collection instance, allowing method chaining.</returns>
    public static ICollection<ISetting> AddSelectionSetting(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        SelectionSettingStyle style = SelectionSettingStyle.Automatic,
        string? defaultValue = null,
        bool isCompact = false,
        bool isDisabled = false,
        params ICollection<ISelectionChoice>? choices)
    {
        var setting = new SelectionSetting(id, title, description, style, defaultValue, isCompact, isDisabled, choices);
        collection.Add(setting);

        return collection;
    }
}
