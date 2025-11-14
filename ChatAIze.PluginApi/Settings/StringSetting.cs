using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a string input setting, allowing users to enter textual data with optional formatting and validation rules.
/// </summary>
public class StringSetting : IStringSetting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringSetting"/> class.
    /// </summary>
    public StringSetting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringSetting"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The display title of the setting.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="placeholder">The placeholder text shown in the input field when empty.</param>
    /// <param name="defaultValue">The default value to be used if the user provides no input.</param>
    /// <param name="textFieldType">The input type (e.g., default, email, URL).</param>
    /// <param name="maxLength">The maximum number of characters allowed in the input.</param>
    /// <param name="editorLines">The number of lines in the input editor (use more than 1 for multiline).</param>
    /// <param name="isLowercase">Whether the input should be automatically converted to lowercase.</param>
    /// <param name="isDisabled">Indicates whether the setting is disabled and not editable.</param>
    [SetsRequiredMembers]
    public StringSetting(
        string id,
        string? title = null,
        string? description = null,
        string? placeholder = null,
        string? defaultValue = null,
        TextFieldType textFieldType = TextFieldType.Default,
        int maxLength = 100,
        int editorLines = 1,
        bool isLowercase = false,
        bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Placeholder = placeholder;
        DefaultValue = defaultValue;
        TextFieldType = textFieldType;
        MaxLength = maxLength;
        EditorLines = editorLines;
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
    public virtual string? Placeholder { get; set; }

    /// <inheritdoc />
    public virtual string? DefaultValue { get; set; }

    /// <inheritdoc />
    public virtual object DefaultValueObject => DefaultValue ?? string.Empty;

    /// <inheritdoc />
    public virtual TextFieldType TextFieldType { get; set; }

    /// <inheritdoc />
    public virtual int MaxLength { get; set; } = 300;

    /// <inheritdoc />
    public virtual int EditorLines { get; set; } = 1;

    /// <inheritdoc />
    public virtual bool IsLowercase { get; set; }

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }
}

/// <summary>
/// Extension methods for adding <see cref="StringSetting"/> instances to setting containers and collections.
/// </summary>
public static class StringSettingExtensions
{
    /// <summary>
    /// Adds a <see cref="StringSetting"/> to an editable settings container.
    /// </summary>
    /// <param name="container">The container to which the setting will be added.</param>
    /// <param name="id">The unique identifier of the setting.</param>
    /// <param name="title">The title of the setting.</param>
    /// <param name="description">Optional description or helper text.</param>
    /// <param name="placeholder">Placeholder text shown when the input field is empty.</param>
    /// <param name="defaultValue">The default value to use.</param>
    /// <param name="textFieldType">The input field type (e.g., password, search, email).</param>
    /// <param name="maxLength">The maximum number of characters allowed.</param>
    /// <param name="editorLines">The number of visible lines (1 for single-line, more for textarea).</param>
    /// <param name="isLowercase">Whether input is forced to lowercase.</param>
    /// <param name="isDisabled">Whether the setting is disabled.</param>
    /// <returns>The same container instance, allowing method chaining.</returns>
    public static IEditableSettingsContainer AddStringSetting(
        this IEditableSettingsContainer container,
        string id,
        string? title = null,
        string? description = null,
        string? placeholder = null,
        string? defaultValue = null,
        TextFieldType textFieldType = TextFieldType.Default,
        int maxLength = 100,
        int editorLines = 1,
        bool isLowercase = false,
        bool isDisabled = false)
    {
        var setting = new StringSetting(id, title, description, placeholder, defaultValue, textFieldType, maxLength, editorLines, isLowercase, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    /// <summary>
    /// Adds a <see cref="StringSetting"/> to a collection of settings.
    /// </summary>
    public static ICollection<ISetting> AddStringSetting(
        this ICollection<ISetting> collection,
        string id,
        string? title = null,
        string? description = null,
        string? placeholder = null,
        string? defaultValue = null,
        TextFieldType textFieldType = TextFieldType.Default,
        int maxLength = 100,
        int editorLines = 1,
        bool isLowercase = false,
        bool isDisabled = false)
    {
        var setting = new StringSetting(id, title, description, placeholder, defaultValue, textFieldType, maxLength, editorLines, isLowercase, isDisabled);
        collection.Add(setting);
        
        return collection;
    }
}
