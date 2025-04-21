using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a setting that allows users to enter a list of string values with optional constraints and input formatting.
/// </summary>
public class ListSetting : IListSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListSetting"/> class.
    /// </summary>
    public ListSetting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListSetting"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">The optional description or helper text.</param>
    /// <param name="itemPlaceholder">The placeholder text shown for each list item.</param>
    /// <param name="textFieldType">The input type (e.g., default, email, URL).</param>
    /// <param name="maxItems">The maximum number of items allowed in the list.</param>
    /// <param name="maxItemLength">The maximum number of characters allowed per item.</param>
    /// <param name="allowDuplicates">Whether duplicate items are allowed.</param>
    /// <param name="isLowercase">Whether to convert all input to lowercase.</param>
    /// <param name="isDisabled">Indicates whether the setting is disabled and not editable.</param>
    [SetsRequiredMembers]
    public ListSetting(
        string id,
        string? title = null,
        string? description = null,
        string? itemPlaceholder = null,
        TextFieldType textFieldType = TextFieldType.Default,
        int maxItems = 100,
        int maxItemLength = 100,
        bool allowDuplicates = false,
        bool isLowercase = false,
        bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        ItemPlaceholder = itemPlaceholder;
        TextFieldType = textFieldType;
        MaxItems = maxItems;
        MaxItemLength = maxItemLength;
        AllowDuplicates = allowDuplicates;
        IsLowercase = isLowercase;
        IsDisabled = isDisabled;
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual string? ItemPlaceholder { get; set; }

    /// <inheritdoc />
    public virtual TextFieldType TextFieldType { get; set; }

    /// <inheritdoc />
    public virtual int MaxItems { get; set; } = 100;

    /// <inheritdoc />
    public virtual int MaxItemLength { get; set; } = 100;

    /// <inheritdoc />
    public virtual object DefaultValueObject => new List<string>();

    /// <inheritdoc />
    public virtual bool AllowDuplicates { get; set; }

    /// <inheritdoc />
    public virtual bool IsLowercase { get; set; }

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }
}

/// <summary>
/// Extension methods for adding <see cref="ListSetting"/> instances to containers and collections.
/// </summary>
public static class ListSettingExtensions
{
    /// <summary>
    /// Adds a <see cref="ListSetting"/> to an editable settings container.
    /// </summary>
    public static IEditableSettingsContainer AddListSetting(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        string? itemPlaceholder = null,
        TextFieldType textFieldType = TextFieldType.Default,
        int maxItems = 100,
        int maxItemLength = 100,
        bool allowDuplicates = false,
        bool isLowercase = false,
        bool isDisabled = false)
    {
        var setting = new ListSetting(id, title, description, itemPlaceholder, textFieldType, maxItems, maxItemLength, allowDuplicates, isLowercase, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    /// <summary>
    /// Adds a <see cref="ListSetting"/> to a settings collection.
    /// </summary>
    public static ICollection<ISetting> AddListSetting(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        string? itemPlaceholder = null,
        TextFieldType textFieldType = TextFieldType.Default,
        int maxItems = 100,
        int maxItemLength = 100,
        bool allowDuplicates = false,
        bool isLowercase = false,
        bool isDisabled = false)
    {
        var setting = new ListSetting(id, title, description, itemPlaceholder, textFieldType, maxItems, maxItemLength, allowDuplicates, isLowercase, isDisabled);
        collection.Add(setting);
        
        return collection;
    }
}
