using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Top-level settings container typically rendered as a "section" with a header and optional description.
/// </summary>
/// <remarks>
/// <para>
/// Sections are a layout construct: they contain other settings (<see cref="Settings"/>) but do not store a value themselves.
/// </para>
/// <para>
/// In ChatAIze.Chatbot, <see cref="Title"/> and <see cref="Description"/> are rendered above the section contents.
/// </para>
/// </remarks>
public class SettingsSection : ISettingsSection, IEditableSettingsContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsSection"/> class.
    /// </summary>
    public SettingsSection() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsSection"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the section.</param>
    /// <param name="title">The title displayed at the top of the section.</param>
    /// <param name="description">Optional description shown below the section title.</param>
    /// <param name="isDisabled">Indicates whether the section and its contents are disabled.</param>
    /// <param name="settings">The settings included in this section.</param>
    [SetsRequiredMembers]
    public SettingsSection(
        string id,
        string? title = null,
        string? description = null,
        bool isDisabled = false,
        params ICollection<ISetting>? settings)
    {
        Id = id;
        Title = title;
        Description = description;
        IsDisabled = isDisabled;
        Settings = settings ?? [];
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }

    /// <summary>
    /// Gets or sets the modifiable collection of settings contained in this section.
    /// </summary>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;
}

/// <summary>
/// Extension methods for adding <see cref="SettingsSection"/> instances to setting containers and collections.
/// </summary>
public static class SettingsSectionExtensions
{
    /// <summary>
    /// Adds a <see cref="SettingsSection"/> to an editable settings container.
    /// </summary>
    /// <param name="container">The settings container to add the section to.</param>
    /// <param name="id">The unique identifier of the section.</param>
    /// <param name="title">The title of the section.</param>
    /// <param name="description">Optional description below the title.</param>
    /// <param name="isDisabled">Indicates whether the section is disabled.</param>
    /// <param name="settings">The settings contained in this section.</param>
    /// <returns>The same container instance, allowing method chaining.</returns>
    /// <remarks>
    /// Sections are layout-only (no stored value). Ensure <paramref name="id"/> is stable so hosts can diff and cache settings trees.
    /// </remarks>
    public static IEditableSettingsContainer AddSettingsSection(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        bool isDisabled = false,
        params ICollection<ISetting>? settings)
    {
        var section = new SettingsSection(id, title, description, isDisabled, settings);
        container.Settings.Add(section);
        
        return container;
    }

    /// <summary>
    /// Adds a <see cref="SettingsSection"/> to a settings collection.
    /// </summary>
    /// <param name="collection">The collection to which the section is added.</param>
    /// <param name="id">The unique identifier of the section.</param>
    /// <param name="title">The title of the section.</param>
    /// <param name="description">Optional description below the title.</param>
    /// <param name="isDisabled">Indicates whether the section is disabled.</param>
    /// <param name="settings">The settings contained in this section.</param>
    /// <returns>The modified settings collection.</returns>
    /// <remarks>
    /// Sections are layout-only (no stored value). Ensure <paramref name="id"/> is stable so hosts can diff and cache settings trees.
    /// </remarks>
    public static ICollection<ISetting> AddSettingsSection(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        bool isDisabled = false,
        params ICollection<ISetting>? settings)
    {
        var section = new SettingsSection(id, title, description, isDisabled, settings);
        collection.Add(section);

        return collection;
    }
}
