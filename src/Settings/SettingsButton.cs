using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a clickable button in the settings UI that performs a custom action when pressed.
/// </summary>
public class SettingsButton : ISettingsButton
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsButton"/> class.
    /// </summary>
    public SettingsButton() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsButton"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the button setting.</param>
    /// <param name="callback">The action to be executed when the button is pressed.</param>
    /// <param name="title">The display title of the button.</param>
    /// <param name="description">Optional description shown below the button.</param>
    /// <param name="style">The visual style of the button.</param>
    /// <param name="isDisabled">Indicates whether the button is disabled in the UI.</param>
    [SetsRequiredMembers]
    public SettingsButton(
        string id,
        Func<CancellationToken, ValueTask> callback,
        string? title = null,
        string? description = null,
        ButtonStyle style = ButtonStyle.Primary,
        bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        IsDisabled = isDisabled;
        Callback = callback;
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual ButtonStyle Style { get; set; }

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }

    /// <inheritdoc />
    public virtual required Func<CancellationToken, ValueTask> Callback { get; set; }
}

/// <summary>
/// Extension methods for adding <see cref="SettingsButton"/> instances to containers and collections.
/// </summary>
public static class SettingsButtonExtensions
{
    /// <summary>
    /// Adds a <see cref="SettingsButton"/> to an editable settings container.
    /// </summary>
    /// <param name="container">The settings container to which the button will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="callback">The action to be executed when the button is clicked.</param>
    /// <param name="title">The display title of the button.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="style">The visual style of the button (e.g., primary, accent, danger).</param>
    /// <param name="isDisabled">Indicates whether the button is disabled in the UI.</param>
    /// <returns>The same container instance, enabling method chaining.</returns>
    public static IEditableSettingsContainer AddSettingsButton(
        this IEditableSettingsContainer container,
        string id,
        Func<CancellationToken, ValueTask> callback,
        string? title = null,
        string? description = null,
        ButtonStyle style = ButtonStyle.Primary,
        bool isDisabled = false)
    {
        var setting = new SettingsButton(id, callback, title, description, style, isDisabled);
        container.Settings.Add(setting);
        
        return container;
    }

    /// <summary>
    /// Adds a <see cref="SettingsButton"/> to a settings collection.
    /// </summary>
    /// <param name="collection">The collection to which the button will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="callback">The action to be executed when the button is clicked.</param>
    /// <param name="title">The display title of the button.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="style">The visual style of the button (e.g., primary, accent, danger).</param>
    /// <param name="isDisabled">Indicates whether the button is disabled in the UI.</param>
    /// <returns>The modified collection of settings.</returns>
    public static ICollection<ISetting> AddSettingsButton(
        this ICollection<ISetting> collection,
        string id,
        Func<CancellationToken, ValueTask> callback,
        string? title = null,
        string? description = null,
        ButtonStyle style = ButtonStyle.Primary,
        bool isDisabled = false)
    {
        var setting = new SettingsButton(id, callback, title, description, style, isDisabled);
        collection.Add(setting);

        return collection;
    }
}
