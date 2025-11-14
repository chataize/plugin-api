using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

/// <inheritdoc cref="ISelectionChoice"/>
public class SelectionChoice : ISelectionChoice
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionChoice"/> class.
    /// </summary>
    public SelectionChoice() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionChoice"/> class with the specified value, title, and disabled state.
    /// </summary>
    /// <param name="value">The value associated with the choice.</param>
    /// <param name="title">The display title shown to users.</param>
    /// <param name="isDisabled">A flag indicating whether the choice is disabled and cannot be selected.</param>
    [SetsRequiredMembers]
    public SelectionChoice(string value, string? title = null, bool isDisabled = false)
    {
        Value = value;
        Title = title;
        IsDisabled = isDisabled;
    }

    /// <inheritdoc />
    public virtual string? Title { get; set; }

    /// <inheritdoc />
    public virtual required string Value { get; set; }

    /// <inheritdoc />
    public bool IsDisabled { get; set; }
}

/// <summary>
/// Provides extension methods for working with <see cref="SelectionSetting"/> and its choices.
/// </summary>
public static class SelectionChoiceExtensions
{
    /// <summary>
    /// Adds a new <see cref="SelectionChoice"/> to the specified <see cref="SelectionSetting"/>.
    /// </summary>
    /// <param name="setting">The selection setting to which the choice will be added.</param>
    /// <param name="value">The internal value of the choice.</param>
    /// <param name="title">The display title of the choice.</param>
    /// <param name="isDisabled">Whether the choice is disabled in the UI.</param>
    public static void AddChoice(this SelectionSetting setting, string value, string? title = null, bool isDisabled = false)
    {
        var choice = new SelectionChoice(value, title, isDisabled);
        setting.Choices.Add(choice);
    }
}
