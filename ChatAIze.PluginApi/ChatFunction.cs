using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ChatAIze.Abstractions;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Utilities.Extensions;

namespace ChatAIze.PluginApi;

/// <summary>
/// Convenience implementation of <see cref="IChatFunction"/> used to expose plugin capabilities as LLM tools.
/// </summary>
/// <remarks>
/// <para>
/// In ChatAIze.Chatbot, functions returned by <see cref="ChatAIze.Abstractions.Plugins.IChatbotPlugin.FunctionsCallback"/> become model-callable tools.
/// When a tool call happens, the host invokes <see cref="IChatFunction.Callback"/> using
/// <see cref="ChatAIze.Utilities.Extensions.DelegateExtensions.InvokeForStringResultAsync(Delegate, string, IFunctionContext?, CancellationToken)"/>.
/// </para>
/// <para>
/// Parameter schema:
/// <list type="bullet">
/// <item><description>If <see cref="Parameters"/> is provided (non-null), the host uses it to build the JSON schema shown to the model.</description></item>
/// <item><description>If <see cref="Parameters"/> is <see langword="null"/>, the schema is derived from <see cref="Callback"/> via reflection.</description></item>
/// </list>
/// </para>
/// <para>
/// If you want full control over what the model sees, prefer supplying an explicit <see cref="Parameters"/> list.
/// Note: the schema serializer excludes <see cref="CancellationToken"/> and ChatAIze context types
/// (<see cref="IChatbotContext"/>, <see cref="IChatContext"/>, <see cref="IConditionContext"/>, <see cref="IFunctionContext"/>,
/// <see cref="IActionContext"/>, <see cref="IUserContext"/>) automatically.
/// </para>
/// <para>
/// When relying on reflection-based schemas, you can improve the model-visible descriptions and constraints by annotating your callback with:
/// <list type="bullet">
/// <item><description><see cref="DescriptionAttribute"/> on the method and/or parameters,</description></item>
/// <item><description>data annotations such as <c>[Required]</c>, <c>[MinLength]</c>, <c>[MaxLength]</c>, <c>[StringLength]</c> on string parameters.</description></item>
/// </list>
/// </para>
/// <para>
/// Return values: if the callback returns a non-string value, the host serializes it to JSON for tool output using snake_case property names.
/// </para>
/// <para>
/// Error handling (provider-specific):
/// tool execution is considered successful by the OpenAI provider when the returned string does not start with <c>"Error:"</c>
/// (case-insensitive). This convention is used to decide whether any tool call "succeeded" in a completion.
/// For recoverable failures, prefer returning <c>"Error: ..."</c> instead of throwing, so the model can retry with corrected input.
/// </para>
/// </remarks>
public class ChatFunction : IChatFunction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChatFunction"/> class.
    /// </summary>
    public ChatFunction() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatFunction"/> class using a delegate.
    /// </summary>
    /// <param name="callback">The delegate that contains the function logic.</param>
    /// <remarks>
    /// The function name is inferred from the delegate method name (using ChatAIze normalization).
    /// If the method is decorated with a <see cref="DescriptionAttribute"/>, its description is used automatically.
    /// </remarks>
    [SetsRequiredMembers]
    public ChatFunction(Delegate callback)
    {
        Name = callback.GetNormalizedMethodName();
        Description = callback.Method.GetCustomAttribute<DescriptionAttribute>()?.Description;
        Callback = callback;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatFunction"/> class with all relevant metadata.
    /// </summary>
    /// <param name="name">The unique name of the function.</param>
    /// <param name="description">Optional description for the function.</param>
    /// <param name="requiresDoubleCheck">A flag indicating whether the model should be asked to confirm by calling the function twice.</param>
    /// <param name="callback">The delegate that contains the function's logic.</param>
    /// <param name="parameters">Optional explicit parameters expected by the function (controls schema generation).</param>
    [SetsRequiredMembers]
    public ChatFunction(
        string name,
        string? description = null,
        bool requiresDoubleCheck = false,
        Delegate? callback = null,
        params ICollection<IFunctionParameter>? parameters)
    {
        Name = name;
        Description = description;
        RequiresDoubleCheck = requiresDoubleCheck;
        Callback = callback;
        Parameters = parameters;
    }

    /// <inheritdoc />
    /// <remarks>
    /// In ChatAIze.Chatbot the name is normalized to snake_case when exposed to the model.
    /// Choose a stable, unique name across all installed plugins.
    /// </remarks>
    public virtual required string Name { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    /// <remarks>
    /// In the OpenAI and Gemini providers, enabling this flag causes an extra model round-trip:
    /// the model is instructed to call the function again to confirm intent before the callback is executed.
    /// </remarks>
    public virtual bool RequiresDoubleCheck { get; set; }

    /// <inheritdoc />
    public virtual Delegate? Callback { get; set; }

    /// <summary>
    /// Gets or sets the list of parameters expected by this function.
    /// </summary>
    /// <remarks>
    /// Provide this list when you want full control over:
    /// <list type="bullet">
    /// <item><description>names and descriptions shown to the model,</description></item>
    /// <item><description>which parameters are required,</description></item>
    /// <item><description>enum value lists,</description></item>
    /// <item><description>and overriding reflection-based inference.</description></item>
    /// </list>
    /// When <see langword="null"/>, the host derives the schema from <see cref="Callback"/> parameters.
    /// </remarks>
    public virtual ICollection<IFunctionParameter>? Parameters { get; set; }

    /// <inheritdoc />
    IReadOnlyCollection<IFunctionParameter>? IChatFunction.Parameters => (IReadOnlyCollection<IFunctionParameter>?)Parameters;
}
