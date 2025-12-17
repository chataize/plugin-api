using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

/// <summary>
/// Describes a reusable workflow action that can be used by ChatAIze.Chatbot "integration functions".
/// </summary>
/// <remarks>
/// <para>
/// Actions are not exposed to the language model as tools. Instead, they are executed by the ChatAIze.Chatbot workflow engine
/// as steps within an integration function.
/// </para>
/// <para>
/// Action <see cref="Id"/> values are persisted in integration function definitions. Treat them as stable identifiers and keep them
/// globally unique across all installed plugins (for example: <c>"com.example.my_plugin:actions.send_invoice"</c>).
/// </para>
/// <para>
/// Execution in ChatAIze.Chatbot:
/// <list type="bullet">
/// <item><description>The action's configuration is a <see cref="JsonElement"/> dictionary keyed by setting ids.</description></item>
/// <item><description>The host invokes <see cref="Callback"/> via <see cref="ChatAIze.Utilities.Extensions.DelegateExtensions.InvokeForStringResultAsync(Delegate, IReadOnlyDictionary{string, JsonElement}, IActionContext?, CancellationToken)"/>.</description></item>
/// <item><description><see cref="IActionContext"/> and <see cref="CancellationToken"/> parameters can be injected by type; other parameters are bound by exact name or snake_case name.</description></item>
/// </list>
/// </para>
/// <para>
/// Settings templating:
/// In ChatAIze.Chatbot, action settings are placeholder-expanded before <see cref="Callback"/> is invoked.
/// Placeholders use <c>{key}</c> syntax (keys are normalized to snake_case). For structured JSON (objects/arrays), the host performs
/// plain text substitution on the raw JSON and reparses it, so placeholders must produce valid JSON (no escaping is performed).
/// When placeholders refer to JSON objects, templates can also reference nested properties using <c>{placeholder.sub_property}</c>.
/// Nested property names are matched as snake_case.
/// </para>
/// <para>
/// Success/failure:
/// returning a string like <c>"Error: ..."</c> does not automatically mark an action as failed. To fail an action, call
/// <see cref="IActionContext.SetActionResult"/> (or let the host mark it as failed due to missing/invalid required settings).
/// </para>
/// <para>
/// Throwing exceptions is caught by ChatAIze.Chatbot and recorded as a failed action result; in non-preview runs, the error message may be
/// replaced with a generic message. Prefer returning a user-friendly message and setting the action result explicitly for predictable behavior.
/// </para>
/// <para>
/// Placeholders:
/// actions can emit placeholder values for later steps via <see cref="IActionContext.SetPlaceholder(string, object)"/> or
/// <see cref="IActionContext.SetPlaceholder(string, JsonElement)"/>.
/// Declare which placeholder ids you intend to write using <see cref="Placeholders"/> / <see cref="PlaceholdersCallback"/>.
/// In ChatAIze.Chatbot ids are normalized to snake_case and may be suffixed (<c>_2</c>, <c>_3</c>, …) to avoid collisions when multiple
/// actions declare the same placeholder.
/// </para>
/// <para>
/// Declare placeholders accurately: the host uses <see cref="PlaceholdersCallback"/> to compute suffixes and UI labels, so over-declaring
/// placeholders can lead to unnecessary suffixing.
/// </para>
/// </remarks>
public class FunctionAction : IFunctionAction, ISettingsContainer, IEditableSettingsContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionAction"/> class.
    /// </summary>
    /// <remarks>
    /// <see cref="SettingsCallback"/> and <see cref="PlaceholdersCallback"/> default to returning the in-memory
    /// <see cref="Settings"/> and <see cref="Placeholders"/> collections.
    /// </remarks>
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
    /// <remarks>
    /// Prefer using setting ids that are valid C# identifier names (letters/digits/underscores) when you want to bind them directly
    /// to delegate parameters.
    /// </remarks>
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
    /// <remarks>
    /// Placeholders should be authored in snake_case to match how ChatAIze.Chatbot normalizes placeholder ids.
    /// </remarks>
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
    /// <remarks>
    /// In ChatAIze.Chatbot, these settings are shown in the dashboard when the action is added to an integration function.
    /// Defaults are taken from <see cref="IDefaultValueObject.DefaultValueObject"/> when a setting implements <see cref="IDefaultValueObject"/>.
    /// </remarks>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;

    /// <summary>
    /// Gets or sets the list of placeholders this action will produce upon execution.
    /// </summary>
    /// <remarks>
    /// This list is used as a declaration/hint for the host. During execution, placeholders are written via
    /// <see cref="IActionContext.SetPlaceholder(string, object)"/> or <see cref="IActionContext.SetPlaceholder(string, JsonElement)"/>.
    /// </remarks>
    public virtual ICollection<string> Placeholders { get; set; } = [];

    /// <inheritdoc />
    /// <remarks>
    /// The host passes the current settings dictionary for a specific action placement.
    /// Use this to render dynamic settings (for example: conditional fields).
    /// </remarks>
    public virtual Func<IReadOnlyDictionary<string, JsonElement>, IReadOnlyCollection<ISetting>> SettingsCallback { get; set; }

    /// <inheritdoc />
    /// <remarks>
    /// The host passes the current settings dictionary for a specific action placement.
    /// Return the placeholder ids this placement may produce so the host can avoid collisions (for example by suffixing ids).
    /// </remarks>
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
