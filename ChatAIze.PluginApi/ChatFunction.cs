using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Utilities.Extensions;

namespace ChatAIze.PluginApi;

/// <summary>
/// Represents a chatbot function implementation that includes its name, description, execution delegate, parameters, and behavioral flags.
/// </summary>
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
    /// The function name is inferred from the delegate method name. If the method is decorated with a <see cref="DescriptionAttribute"/>,
    /// its description is used automatically.
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
    /// <param name="requiresDoubleCheck">A flag indicating whether double confirmation is required before execution.</param>
    /// <param name="callback">The delegate that contains the function's logic.</param>
    /// <param name="parameters">Optional parameters expected by the function.</param>
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
    public virtual required string Name { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual bool RequiresDoubleCheck { get; set; }

    /// <inheritdoc />
    public virtual Delegate? Callback { get; set; }

    /// <summary>
    /// Gets or sets the list of parameters expected by this function.
    /// </summary>
    public virtual ICollection<IFunctionParameter>? Parameters { get; set; }

    /// <inheritdoc />
    IReadOnlyCollection<IFunctionParameter>? IChatFunction.Parameters => (IReadOnlyCollection<IFunctionParameter>?)Parameters;
}
