using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

/// <summary>
/// Represents a condition that must be satisfied before a chatbot function can be executed.
/// Includes logic, configuration metadata, and support for dashboard integration.
/// </summary>
public class FunctionCondition : IFunctionCondition, ISettingsContainer, IEditableSettingsContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionCondition"/> class.
    /// </summary>
    public FunctionCondition()
    {
        SettingsCallback ??= _ => (IReadOnlyCollection<ISetting>)Settings;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionCondition"/> class with the specified metadata and logic.
    /// </summary>
    /// <param name="id">The unique identifier of the condition.</param>
    /// <param name="title">The display title used in the dashboard.</param>
    /// <param name="callback">The delegate containing the evaluation logic.</param>
    /// <param name="description">Optional description to clarify the purpose of the condition.</param>
    /// <param name="iconUrl">Optional icon URL for visual representation in the dashboard.</param>
    /// <param name="settings">Optional settings for configuring the condition behavior.</param>
    [SetsRequiredMembers]
    public FunctionCondition(
        string id,
        string title,
        Delegate callback,
        string? description = null,
        string? iconUrl = null,
        params ICollection<ISetting>? settings)
        : this()
    {
        Id = id;
        Title = title;
        Description = description;
        IconUrl = iconUrl;
        Callback = callback;
        Settings = settings ?? [];
    }

    /// <inheritdoc />
    public virtual required string Id { get; set; }

    /// <inheritdoc />
    public virtual required string Title { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual string? IconUrl { get; set; }

    /// <inheritdoc />
    public virtual required Delegate Callback { get; set; }

    /// <summary>
    /// Gets or sets the collection of configurable settings for this condition.
    /// </summary>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;

    /// <inheritdoc />
    public virtual Func<IReadOnlyDictionary<string, JsonElement>, IReadOnlyCollection<ISetting>> SettingsCallback { get; set; }

    /// <summary>
    /// Adds a setting to the condition's configurable options.
    /// </summary>
    /// <param name="setting">The setting to add.</param>
    public void AddSetting(ISetting setting)
    {
        Settings.Add(setting);
    }
}
