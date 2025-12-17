using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a static text paragraph displayed in the settings UI, typically used for instructions or explanations.
/// </summary>
/// <remarks>
/// Paragraphs are a layout construct: they do not store a value, but they can provide context and guidance next to real settings.
/// </remarks>
public class SettingsParagraph : ISettingsParagraph
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsParagraph"/> class.
    /// </summary>
    public SettingsParagraph() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsParagraph"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the paragraph.</param>
    /// <param name="content">The content of the paragraph to display as static text.</param>
    /// <param name="isDisabled">Indicates whether the paragraph should be hidden or grayed out in the UI.</param>
    [SetsRequiredMembers]
    public SettingsParagraph(string id, string? content = null, bool isDisabled = false)
    {
        Id = id;
        Content = content;
        IsDisabled = isDisabled;
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual string? Content { get; set; }

    /// <inheritdoc />
    public virtual bool IsDisabled { get; set; }
}

/// <summary>
/// Extension methods for adding <see cref="SettingsParagraph"/> instances to setting containers and collections.
/// </summary>
public static class SettingsParagraphExtensions
{
    /// <summary>
    /// Adds a <see cref="SettingsParagraph"/> to an editable settings container.
    /// </summary>
    /// <param name="container">The container to which the paragraph will be added.</param>
    /// <param name="id">The unique identifier of the paragraph.</param>
    /// <param name="content">The paragraph content displayed as static text.</param>
    /// <param name="isDisabled">Indicates whether the paragraph is shown as disabled.</param>
    /// <returns>The modified settings container.</returns>
    /// <remarks>
    /// Paragraphs are layout-only (no stored value). Ensure <paramref name="id"/> is stable so hosts can diff and cache settings trees.
    /// </remarks>
    public static IEditableSettingsContainer AddSettingsParagraph(
        this IEditableSettingsContainer container,
        string id,
        string? content = null,
        bool isDisabled = false)
    {
        var paragraph = new SettingsParagraph(id, content, isDisabled);
        container.Settings.Add(paragraph);
        
        return container;
    }

    /// <summary>
    /// Adds a <see cref="SettingsParagraph"/> to a collection of settings.
    /// </summary>
    /// <param name="collection">The collection to which the paragraph will be added.</param>
    /// <param name="id">The unique identifier of the paragraph.</param>
    /// <param name="content">The paragraph content displayed as static text.</param>
    /// <param name="isDisabled">Indicates whether the paragraph is shown as disabled.</param>
    /// <returns>The modified collection of settings.</returns>
    /// <remarks>
    /// Paragraphs are layout-only (no stored value). Ensure <paramref name="id"/> is stable so hosts can diff and cache settings trees.
    /// </remarks>
    public static ICollection<ISetting> AddSettingsParagraph(
        this ICollection<ISetting> collection,
        string id,
        string? content = null,
        bool isDisabled = false)
    {
        var paragraph = new SettingsParagraph(id, content, isDisabled);
        collection.Add(paragraph);

        return collection;
    }
}
