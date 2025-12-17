using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

/// <summary>
/// Convenience base implementation of <see cref="IChatbotPlugin"/> for ChatAIze.Chatbot plugins.
/// </summary>
/// <remarks>
/// <para>
/// In ChatAIze.Chatbot, plugins are loaded from a DLL in the <c>plugins</c> folder (or uploaded through the dashboard).
/// The host discovers a plugin in this order:
/// <list type="number">
/// <item><description>a type implementing <see cref="IAsyncPluginLoader"/> (async factory),</description></item>
/// <item><description>a type implementing <see cref="IPluginLoader"/> (sync factory),</description></item>
/// <item><description>any non-abstract type implementing <see cref="IChatbotPlugin"/> (created via <c>Activator.CreateInstance</c>).</description></item>
/// </list>
/// </para>
/// <para>
/// A plugin instance is typically a singleton for the lifetime of the host process. Your callbacks and delegates can be invoked
/// concurrently for multiple chats/users. Avoid storing per-user/per-chat state on the plugin instance; prefer using the
/// supplied <see cref="IChatContext"/>, <see cref="IChatbotContext"/>, <see cref="IFunctionContext"/>, <see cref="IActionContext"/>,
/// and <see cref="IConditionContext"/> objects.
/// </para>
/// <para>
/// Settings in ChatAIze.Chatbot are stored in a single key/value dictionary (<see cref="IPluginSettings"/>). Because keys are shared
/// across all plugins, use stable, globally-unique setting ids (for example: <c>"com.example.my_plugin:api_key"</c>).
/// </para>
/// </remarks>
public class ChatbotPlugin : IChatbotPlugin, IEditableSettingsContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChatbotPlugin"/> class with default callbacks.
    /// </summary>
    /// <remarks>
    /// The default behavior of this type is "static": each callback returns the corresponding in-memory collection
    /// (<see cref="Settings"/>, <see cref="Functions"/>, <see cref="Actions"/>, <see cref="Conditions"/>).
    /// Override the callback properties if you need context-dependent definitions.
    /// </remarks>
    public ChatbotPlugin()
    {
        SettingsCallback ??= _ => ValueTask.FromResult((IReadOnlyCollection<ISetting>)Settings);
        FunctionsCallback ??= _ => ValueTask.FromResult((IReadOnlyCollection<IChatFunction>)Functions);
        ActionsCallback ??= _ => ValueTask.FromResult((IReadOnlyCollection<IFunctionAction>)Actions);
        ConditionsCallback ??= _ => ValueTask.FromResult((IReadOnlyCollection<IFunctionCondition>)Conditions);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatbotPlugin"/> class with metadata and initial collections.
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
    /// <param name="settings">Optional collection of plugin-level settings (rendered in the dashboard).</param>
    /// <param name="functions">Optional collection of chatbot functions (tools) provided by the plugin.</param>
    /// <param name="actions">Optional collection of reusable workflow actions provided by the plugin.</param>
    /// <param name="conditions">Optional collection of reusable workflow conditions provided by the plugin.</param>
    /// <remarks>
    /// In ChatAIze.Chatbot:
    /// <list type="bullet">
    /// <item><description><see cref="SettingsCallback"/> is used to render plugin settings in the dashboard.</description></item>
    /// <item><description><see cref="FunctionsCallback"/> is used to surface functions as LLM tools (and executed via <see cref="ChatAIze.Utilities.Extensions.DelegateExtensions"/> when <see cref="IChatFunction.Callback"/> is provided).</description></item>
    /// <item><description><see cref="ActionsCallback"/> / <see cref="ConditionsCallback"/> are used by the workflow engine.</description></item>
    /// </list>
    /// </remarks>
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
    /// <remarks>
    /// In ChatAIze.Chatbot, settings are rendered in the dashboard and persisted as JSON under <see cref="ISetting.Id"/>.
    /// Prefer stable ids and prefix them with your plugin id to avoid collisions with other plugins.
    /// </remarks>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of chatbot functions exposed by the plugin.
    /// </summary>
    /// <remarks>
    /// These functions are surfaced to the language model as callable tools.
    /// <para>
    /// In ChatAIze.Chatbot, a function is only executable if <see cref="IChatFunction.Callback"/> is set (otherwise the host falls back
    /// to a default callback intended for "integration functions" configured in the dashboard).
    /// </para>
    /// </remarks>
    public virtual ICollection<IChatFunction> Functions { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of reusable workflow actions provided by the plugin.
    /// </summary>
    /// <remarks>
    /// Actions are building blocks used by ChatAIze.Chatbot "integration functions". They are not surfaced to the model directly.
    /// </remarks>
    public virtual ICollection<IFunctionAction> Actions { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of reusable workflow conditions provided by the plugin.
    /// </summary>
    /// <remarks>
    /// Conditions can be attached to integration functions to allow/deny execution based on settings and chat context.
    /// </remarks>
    public virtual ICollection<IFunctionCondition> Conditions { get; set; } = [];

    /// <inheritdoc />
    /// <remarks>
    /// In ChatAIze.Chatbot this callback is invoked while rendering the plugin settings page and when plugin settings change.
    /// It should be fast and should return deterministic ids.
    /// </remarks>
    public virtual Func<IChatbotContext, ValueTask<IReadOnlyCollection<ISetting>>> SettingsCallback { get; set; }

    /// <inheritdoc />
    /// <remarks>
    /// In ChatAIze.Chatbot this callback is invoked when building the tool list for a completion. Return only functions that should be
    /// available for the current chat/user.
    /// </remarks>
    public virtual Func<IChatContext, ValueTask<IReadOnlyCollection<IChatFunction>>> FunctionsCallback { get; set; }

    /// <inheritdoc />
    /// <remarks>
    /// In ChatAIze.Chatbot this callback is invoked when the dashboard or workflow engine needs the list of available actions.
    /// </remarks>
    public virtual Func<IChatbotContext, ValueTask<IReadOnlyCollection<IFunctionAction>>> ActionsCallback { get; set; }

    /// <inheritdoc />
    /// <remarks>
    /// In ChatAIze.Chatbot this callback is invoked when the dashboard or workflow engine needs the list of available conditions.
    /// </remarks>
    public virtual Func<IChatbotContext, ValueTask<IReadOnlyCollection<IFunctionCondition>>> ConditionsCallback { get; set; }

    /// <summary>
    /// Adds a setting definition to <see cref="Settings"/>.
    /// </summary>
    /// <param name="setting">The setting to add.</param>
    /// <remarks>
    /// Note: the method name contains a historical typo (<c>AddSetttng</c>). It is kept for backwards compatibility.
    /// You can also add settings via <see cref="IEditableSettingsContainer.Settings"/> or the extension methods in
    /// <see cref="ChatAIze.PluginApi.Settings.StringSettingExtensions"/>.
    /// </remarks>
    public virtual void AddSetttng(ISetting setting)
    {
        Settings.Add(setting);
    }

    /// <summary>
    /// Adds a chatbot function (tool) to <see cref="Functions"/>.
    /// </summary>
    /// <param name="function">The function to register.</param>
    /// <remarks>
    /// In ChatAIze.Chatbot, functions should provide a non-null <see cref="IChatFunction.Callback"/> for the host to execute them.
    /// </remarks>
    public virtual void AddFunction(IChatFunction function)
    {
        Functions.Add(function);
    }

    /// <summary>
    /// Adds a chatbot function by wrapping a raw delegate.
    /// </summary>
    /// <param name="function">The function delegate to wrap.</param>
    /// <remarks>
    /// This overload creates a <see cref="ChatFunction"/> which:
    /// <list type="bullet">
    /// <item><description>derives <see cref="IChatFunction.Name"/> from the method name,</description></item>
    /// <item><description>uses <see cref="System.ComponentModel.DescriptionAttribute"/> (when present) as the description,</description></item>
    /// <item><description>stores the delegate as <see cref="IChatFunction.Callback"/> so the host can execute it.</description></item>
    /// </list>
    /// The delegate is invoked using ChatAIze's standard binding rules (snake_case arguments, optional <see cref="IFunctionContext"/> and <see cref="CancellationToken"/> injection).
    /// <para>
    /// For best results, prefer using a named method. If you pass a lambda or local function, the compiler-generated method name may be unstable;
    /// ChatAIze attempts to normalize such names but can throw if the pattern is not recognized. If you need full control over the tool name,
    /// construct a <see cref="ChatFunction"/> with an explicit <see cref="IChatFunction.Name"/> instead.
    /// </para>
    /// </remarks>
    public virtual void AddFunction(Delegate function)
    {
        Functions.Add(new ChatFunction(function));
    }

    /// <summary>
    /// Adds a reusable workflow action to <see cref="Actions"/>.
    /// </summary>
    /// <param name="action">The function action to register.</param>
    public virtual void AddAction(IFunctionAction action)
    {
        Actions.Add(action);
    }

    /// <summary>
    /// Adds a reusable workflow condition to <see cref="Conditions"/>.
    /// </summary>
    /// <param name="condition">The function condition to register.</param>
    public virtual void AddCondition(IFunctionCondition condition)
    {
        Conditions.Add(condition);
    }
}
