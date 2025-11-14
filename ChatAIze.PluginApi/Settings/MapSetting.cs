using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a dictionary-like setting that allows users to input key-value pairs with optional constraints.
/// </summary>
public class MapSetting : IMapSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapSetting"/> class.
    /// </summary>
    public MapSetting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MapSetting"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="keyPlaceholder">Placeholder text for key input fields.</param>
    /// <param name="valuePlaceholder">Placeholder text for value input fields.</param>
    /// <param name="maxItems">Maximum number of key-value pairs allowed.</param>
    /// <param name="maxKeyLength">Maximum length of each key.</param>
    /// <param name="maxValueLength">Maximum length of each value.</param>
    /// <param name="isDisabled">Indicates whether the setting is disabled and not editable.</param>
    [SetsRequiredMembers]
    public MapSetting(
        string id,
        string? title = null,
        string? description = null,
        string? keyPlaceholder = null,
        string? valuePlaceholder = null,
        int maxItems = 100,
        int maxKeyLength = 100,
        int maxValueLength = 100,
        bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        KeyPlaceholder = keyPlaceholder;
        ValuePlaceholder = valuePlaceholder;
        MaxItems = maxItems;
        MaxKeyLength = maxKeyLength;
        MaxValueLength = maxValueLength;
        IsDisabled = isDisabled;
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual string? KeyPlaceholder { get; set; }

    /// <inheritdoc />
    public virtual string? ValuePlaceholder { get; set; }

    /// <inheritdoc />
    public virtual int MaxItems { get; set; } = 100;

    /// <inheritdoc />
    public virtual int MaxKeyLength { get; set; } = 100;

    /// <inheritdoc />
    public virtual int MaxValueLength { get; set; } = 100;

    /// <inheritdoc />
    public virtual object DefaultValueObject => new Dictionary<string, string>();

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }
}

/// <summary>
/// Extension methods for adding <see cref="MapSetting"/> instances to setting containers and collections.
/// </summary>
public static class MapSettingExtensions
{
    /// <summary>
    /// Adds a <see cref="MapSetting"/> to an editable settings container.
    /// </summary>
    public static IEditableSettingsContainer AddMapSetting(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        string? keyPlaceholder = null,
        string? valuePlaceholder = null,
        int maxItems = 100,
        int maxKeyLength = 100,
        int maxValueLength = 100,
        bool isDisabled = false)
    {
        var setting = new MapSetting(id, title, description, keyPlaceholder, valuePlaceholder, maxItems, maxKeyLength, maxValueLength, isDisabled);
        container.Settings.Add(setting);
        
        return container;
    }

    /// <summary>
    /// Adds a <see cref="MapSetting"/> to a settings collection.
    /// </summary>
    public static ICollection<ISetting> AddMapSetting(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        string? keyPlaceholder = null,
        string? valuePlaceholder = null,
        int maxItems = 100,
        int maxKeyLength = 100,
        int maxValueLength = 100,
        bool isDisabled = false)
    {
        var setting = new MapSetting(id, title, description, keyPlaceholder, valuePlaceholder, maxItems, maxKeyLength, maxValueLength, isDisabled);
        collection.Add(setting);

        return collection;
    }
}
