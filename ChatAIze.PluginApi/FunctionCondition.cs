using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

/// <summary>
/// Describes a reusable workflow condition used to allow/deny execution of ChatAIze.Chatbot "integration functions".
/// </summary>
/// <remarks>
/// <para>
/// Conditions are evaluated by the ChatAIze.Chatbot workflow engine before an integration function executes.
/// They are not exposed to the language model as tools.
/// </para>
/// <para>
/// Condition <see cref="Id"/> values are persisted in integration function definitions. Treat them as stable identifiers and keep them
/// globally unique across all installed plugins (for example: <c>"com.example.my_plugin:conditions.is_premium_user"</c>).
/// </para>
/// <para>
/// Execution in ChatAIze.Chatbot:
/// <list type="bullet">
/// <item><description>The condition receives a settings dictionary keyed by setting ids.</description></item>
/// <item><description>The host invokes <see cref="Callback"/> via <see cref="ChatAIze.Utilities.Extensions.DelegateExtensions.InvokeForConditionResultAsync(Delegate, IReadOnlyDictionary{string, JsonElement}, IConditionContext?, CancellationToken)"/>.</description></item>
/// <item><description><see cref="IConditionContext"/> and <see cref="CancellationToken"/> parameters can be injected by type; other parameters are bound by exact name or snake_case name.</description></item>
/// </list>
/// </para>
/// <para>
/// Settings templating:
/// In ChatAIze.Chatbot, condition settings are placeholder-expanded using the integration function's parameter values before <see cref="Callback"/> is invoked.
/// Placeholders use <c>{parameter_name}</c> syntax (parameter names are normalized to snake_case). Substitution is plain text; when placeholders are used inside
/// JSON object/array settings, the resulting JSON must still be valid (no escaping is performed).
/// </para>
/// <para>
/// Return conventions:
/// <list type="bullet">
/// <item><description>Return <see langword="true"/> to allow execution.</description></item>
/// <item><description>Return <see langword="false"/> to deny execution without a reason.</description></item>
/// <item><description>Return a string (or any other value) to deny execution with a reason (non-string values are JSON-serialized).</description></item>
/// </list>
/// </para>
/// <para>
/// In ChatAIze.Chatbot, when a condition denies execution, the reason (if provided) is surfaced as an error string returned to the model/user.
/// Prefer returning a clear string message for user-facing denials rather than throwing exceptions.
/// </para>
/// </remarks>
public class FunctionCondition : IFunctionCondition, ISettingsContainer, IEditableSettingsContainer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionCondition"/> class.
    /// </summary>
    /// <remarks>
    /// <see cref="SettingsCallback"/> defaults to returning the in-memory <see cref="Settings"/> collection.
    /// </remarks>
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
    /// <remarks>
    /// Prefer using setting ids that are valid C# identifier names (letters/digits/underscores) when you want to bind them directly
    /// to delegate parameters.
    /// </remarks>
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
    /// <remarks>
    /// In ChatAIze.Chatbot, these settings are shown in the dashboard when the condition is attached to an integration function.
    /// Defaults are taken from <see cref="IDefaultValueObject.DefaultValueObject"/> when a setting implements <see cref="IDefaultValueObject"/>.
    /// </remarks>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;

    /// <inheritdoc />
    /// <remarks>
    /// The host passes the current settings dictionary for a specific condition placement.
    /// Use this to render dynamic settings (for example: conditional fields).
    /// </remarks>
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
