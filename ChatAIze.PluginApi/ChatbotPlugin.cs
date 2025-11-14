using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

/// <summary>
/// Represents a chatbot plugin implementation that provides settings, functions, actions, and conditions to extend chatbot capabilities.
/// </summary>
public class ChatbotPlugin : IChatbotPlugin, IEditableSettingsContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChatbotPlugin"/> class with default callbacks.
    /// </summary>
    public ChatbotPlugin()
    {
        SettingsCallback ??= _ => ValueTask.FromResult((IReadOnlyCollection<ISetting>)Settings);
        FunctionsCallback ??= _ => ValueTask.FromResult((IReadOnlyCollection<IChatFunction>)Functions);
        ActionsCallback ??= _ => ValueTask.FromResult((IReadOnlyCollection<IFunctionAction>)Actions);
        ConditionsCallback ??= _ => ValueTask.FromResult((IReadOnlyCollection<IFunctionCondition>)Conditions);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatbotPlugin"/> class with metadata and content.
    /// </summary>
    /// <param name="id">The unique identifier of the plugin.</param>
    /// <param name="title">The display title of the plugin.</param>
    /// <param name="description">Optional description of the plugin's purpose.</param>
    /// <param name="iconUrl">Optional icon URL to represent the plugin visually in the UI.</param>
    /// <param name="website">Optional website link for the plugin or its author.</param>
    /// <param name="author">Optional name of the plugin's author.</param>
    /// <param name="version">Optional version information (defaults to 1.0.0.0).</param>
    /// <param name="releaseTime">Optional initial release timestamp.</param>
    /// <param name="lastUpdateTime">Optional timestamp of the most recent update.</param>
    /// <param name="settings">Optional collection of plugin-level settings.</param>
    /// <param name="functions">Optional collection of chatbot functions provided by the plugin.</param>
    /// <param name="actions">Optional collection of reusable function actions.</param>
    /// <param name="conditions">Optional collection of conditional rules used before function execution.</param>
    [SetsRequiredMembers]
    public ChatbotPlugin(
        string id,
        string title,
        string? description = null,
        string? iconUrl = null,
        string? website = null,
        string? author = null,
        Version? version = null,
        DateTimeOffset? releaseTime = null,
        DateTimeOffset? lastUpdateTime = null,
        ICollection<ISetting>? settings = null,
        ICollection<IChatFunction>? functions = null,
        ICollection<IFunctionAction>? actions = null,
        ICollection<IFunctionCondition>? conditions = null)
        : this()
    {
        Id = id;
        Title = title;
        Description = description;
        IconUrl = iconUrl;
        Website = website;
        Author = author;
        Version = version ?? new Version(1, 0, 0, 0);
        ReleaseTime = releaseTime;
        LastUpdateTime = lastUpdateTime;
        Settings = settings ?? [];
        Functions = functions ?? [];
        Actions = actions ?? [];
        Conditions = conditions ?? [];
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
    public virtual string? Website { get; set; }

    /// <inheritdoc />
    public virtual string? Author { get; set; }

    /// <inheritdoc />
    public virtual Version Version { get; set; } = new Version(1, 0, 0, 0);

    /// <inheritdoc />
    public virtual DateTimeOffset? ReleaseTime { get; set; }

    /// <inheritdoc />
    public virtual DateTimeOffset? LastUpdateTime { get; set; }

    /// <summary>
    /// Gets or sets the collection of settings exposed by the plugin.
    /// </summary>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of chatbot functions exposed by the plugin.
    /// </summary>
    public virtual ICollection<IChatFunction> Functions { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of reusable function actions provided by the plugin.
    /// </summary>
    public virtual ICollection<IFunctionAction> Actions { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of preconditions that can be applied before function execution.
    /// </summary>
    public virtual ICollection<IFunctionCondition> Conditions { get; set; } = [];

    /// <inheritdoc />
    public virtual Func<IChatbotContext, ValueTask<IReadOnlyCollection<ISetting>>> SettingsCallback { get; set; }

    /// <inheritdoc />
    public virtual Func<IChatContext, ValueTask<IReadOnlyCollection<IChatFunction>>> FunctionsCallback { get; set; }

    /// <inheritdoc />
    public virtual Func<IChatbotContext, ValueTask<IReadOnlyCollection<IFunctionAction>>> ActionsCallback { get; set; }

    /// <inheritdoc />
    public virtual Func<IChatbotContext, ValueTask<IReadOnlyCollection<IFunctionCondition>>> ConditionsCallback { get; set; }

    /// <summary>
    /// Adds a setting to the plugin.
    /// </summary>
    /// <param name="setting">The setting to add.</param>
    public virtual void AddSetttng(ISetting setting)
    {
        Settings.Add(setting);
    }

    /// <summary>
    /// Adds a chatbot function to the plugin.
    /// </summary>
    /// <param name="function">The function to register.</param>
    public virtual void AddFunction(IChatFunction function)
    {
        Functions.Add(function);
    }

    /// <summary>
    /// Adds a chatbot function by wrapping a raw delegate.
    /// </summary>
    /// <param name="function">The function delegate to wrap.</param>
    public virtual void AddFunction(Delegate function)
    {
        Functions.Add(new ChatFunction(function));
    }

    /// <summary>
    /// Adds a reusable action to the plugin.
    /// </summary>
    /// <param name="action">The function action to register.</param>
    public virtual void AddAction(IFunctionAction action)
    {
        Actions.Add(action);
    }

    /// <summary>
    /// Adds a condition that can be used to restrict function execution.
    /// </summary>
    /// <param name="condition">The function condition to register.</param>
    public virtual void AddCondition(IFunctionCondition condition)
    {
        Conditions.Add(condition);
    }
}
