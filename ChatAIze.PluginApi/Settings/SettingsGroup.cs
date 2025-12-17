using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Nested settings container typically rendered as a "group" or subheading within a section.
/// </summary>
/// <remarks>
/// Groups are a layout construct: they contain other settings (<see cref="Settings"/>) but do not store a value themselves.
/// In ChatAIze.Chatbot, groups render a subheader and optional description above their contents.
/// </remarks>
public class SettingsGroup : ISettingsGroup, IEditableSettingsContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsGroup"/> class.
    /// </summary>
    public SettingsGroup() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsGroup"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the group.</param>
    /// <param name="title">The display title of the group.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="isDisabled">Indicates whether the group is disabled and its contents should not be editable.</param>
    /// <param name="settings">The settings contained within this group.</param>
    [SetsRequiredMembers]
    public SettingsGroup(
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
    /// Gets or sets the collection of settings contained within the group.
    /// </summary>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;
}

/// <summary>
/// Extension methods for adding <see cref="SettingsGroup"/> instances to setting containers and collections.
/// </summary>
public static class SettingsGroupExtensions
{
    /// <summary>
    /// Adds a <see cref="SettingsGroup"/> to an editable settings container.
    /// </summary>
    /// <param name="container">The settings container to add the group to.</param>
    /// <param name="id">The unique identifier of the group.</param>
    /// <param name="title">The display title of the group.</param>
    /// <param name="description">Optional helper text displayed under the title.</param>
    /// <param name="isDisabled">Whether the group should be disabled.</param>
    /// <param name="settings">The settings contained within the group.</param>
    /// <returns>The same container instance, allowing fluent chaining.</returns>
    /// <remarks>
    /// Groups are layout-only (no stored value). Ensure <paramref name="id"/> is stable so hosts can diff and cache settings trees.
    /// </remarks>
    public static IEditableSettingsContainer AddSettingsGroup(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        bool isDisabled = false,
        params ICollection<ISetting>? settings)
    {
        var setting = new SettingsGroup(id, title, description, isDisabled, settings);
        container.Settings.Add(setting);
        
        return container;
    }

    /// <summary>
    /// Adds a <see cref="SettingsGroup"/> to a collection of settings.
    /// </summary>
    /// <param name="collection">The collection to which the group is added.</param>
    /// <param name="id">The unique identifier of the group.</param>
    /// <param name="title">The display title of the group.</param>
    /// <param name="description">Optional helper text displayed under the title.</param>
    /// <param name="isDisabled">Whether the group should be disabled.</param>
    /// <param name="settings">The settings contained within the group.</param>
    /// <returns>The same collection instance, allowing fluent chaining.</returns>
    /// <remarks>
    /// Groups are layout-only (no stored value). Ensure <paramref name="id"/> is stable so hosts can diff and cache settings trees.
    /// </remarks>
    public static ICollection<ISetting> AddSettingsGroup(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        bool isDisabled = false,
        params ICollection<ISetting>? settings)
    {
        var setting = new SettingsGroup(id, title, description, isDisabled, settings);
        collection.Add(setting);

        return collection;
    }
}
