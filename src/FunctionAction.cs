using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

/// <summary>
/// Represents an action within a chatbot function, including execution logic, metadata, and dynamic configuration behavior.
/// </summary>
public class FunctionAction : IFunctionAction, ISettingsContainer, IEditableSettingsContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionAction"/> class.
    /// </summary>
    public FunctionAction()
    {
        SettingsCallback ??= _ => (IReadOnlyCollection<ISetting>)Settings;
        PlaceholdersCallback ??= _ => (IReadOnlyCollection<string>)Placeholders;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionAction"/> class with pre-defined settings.
    /// </summary>
    /// <param name="id">The unique identifier of the action.</param>
    /// <param name="title">The title used for display purposes.</param>
    /// <param name="callback">The delegate that defines the action's logic.</param>
    /// <param name="description">An optional description for display in the dashboard.</param>
    /// <param name="iconUrl">An optional icon URL for visual representation.</param>
    /// <param name="runsSilently">Indicates whether the result should be hidden from the user.</param>
    /// <param name="settings">The settings used to configure the action dynamically.</param>
    [SetsRequiredMembers]
    [OverloadResolutionPriority(1)]
    public FunctionAction(string id, string title, Delegate callback, string? description = null, string? iconUrl = null, bool runsSilently = false, params ICollection<ISetting>? settings) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        IconUrl = iconUrl;
        RunsSilently = runsSilently;
        Callback = callback;
        Settings = settings ?? [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionAction"/> class with predefined placeholders.
    /// </summary>
    /// <param name="id">The unique identifier of the action.</param>
    /// <param name="title">The title used for display purposes.</param>
    /// <param name="callback">The delegate that defines the action's logic.</param>
    /// <param name="description">An optional description for display in the dashboard.</param>
    /// <param name="iconUrl">An optional icon URL for visual representation.</param>
    /// <param name="runsSilently">Indicates whether the result should be hidden from the user.</param>
    /// <param name="placeholders">A list of output placeholder names produced by this action.</param>
    [SetsRequiredMembers]
    public FunctionAction(string id, string title, Delegate callback, string? description = null, string? iconUrl = null, bool runsSilently = false, params ICollection<string>? placeholders) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        IconUrl = iconUrl;
        RunsSilently = runsSilently;
        Callback = callback;
        Placeholders = placeholders ?? [];
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
    public virtual bool RunsSilently { get; set; }

    /// <inheritdoc />
    public virtual required Delegate Callback { get; set; }

    /// <summary>
    /// Gets or sets the list of settings used to configure this action.
    /// </summary>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;

    /// <summary>
    /// Gets or sets the list of placeholders this action will produce upon execution.
    /// </summary>
    public virtual ICollection<string> Placeholders { get; set; } = [];

    /// <inheritdoc />
    public virtual Func<IReadOnlyDictionary<string, JsonElement>, IReadOnlyCollection<ISetting>> SettingsCallback { get; set; }

    /// <inheritdoc />
    public virtual Func<IReadOnlyDictionary<string, JsonElement>, IReadOnlyCollection<string>> PlaceholdersCallback { get; set; }

    /// <summary>
    /// Adds a new setting to the action.
    /// </summary>
    /// <param name="setting">The setting to add.</param>
    public virtual void AddSetting(ISetting setting)
    {
        Settings.Add(setting);
    }

    /// <summary>
    /// Adds a new placeholder output key to the action.
    /// </summary>
    /// <param name="placeholder">The placeholder name.</param>
    public virtual void AddPlaceholder(string placeholder)
    {
        Placeholders.Add(placeholder);
    }
}
