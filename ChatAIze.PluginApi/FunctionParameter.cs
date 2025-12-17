using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

/// <summary>
/// Concrete <see cref="IFunctionParameter"/> implementation used to describe tool/function parameters.
/// </summary>
/// <remarks>
/// <para>
/// Use this type when you want to provide an explicit <see cref="IChatFunction.Parameters"/> list instead of relying on
/// reflection over <see cref="IChatFunction.Callback"/>.
/// </para>
/// <para>
/// In ChatAIze.Chatbot the schema generator normalizes parameter names to snake_case. For best results:
/// <list type="bullet">
/// <item><description>choose stable names that read well in snake_case,</description></item>
/// <item><description>use <see cref="IsRequired"/> to control the JSON schema <c>required</c> array,</description></item>
/// <item><description>populate <see cref="EnumValues"/> to expose a restricted list of string values to the model.</description></item>
/// </list>
/// </para>
/// <para>
/// Note: <see cref="EnumValues"/> is used for schema generation and guidance. Validation of enum-like inputs at runtime depends on
/// your callback signature and host binding rules.
/// </para>
/// </remarks>
public class FunctionParameter : IFunctionParameter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionParameter"/> class.
    /// </summary>
    public FunctionParameter() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionParameter"/> class with the specified values.
    /// </summary>
    /// <param name="type">The data type of the parameter.</param>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="description">An optional description providing context or guidance for the parameter.</param>
    /// <param name="isRequired">A flag indicating whether the parameter is required.</param>
    /// <param name="enumValues">A list of allowed values if the parameter is modeled as an enum/string union in the schema.</param>
    [SetsRequiredMembers]
    public FunctionParameter(Type type, string name, string? description = null, bool isRequired = false, ICollection<string>? enumValues = null)
    {
        Type = type;
        Name = name;
        Description = description;
        IsRequired = isRequired;
        EnumValues = enumValues ?? [];
    }

    /// <inheritdoc />
    public virtual required Type Type { get; set; }

    /// <inheritdoc />
    /// <remarks>
    /// Parameter names are typically authored in camelCase/PascalCase but are exposed to the model as snake_case.
    /// </remarks>
    public virtual required string Name { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual bool IsRequired { get; set; }

    /// <summary>
    /// Gets or sets the list of allowed values to expose in the JSON schema <c>enum</c> array.
    /// </summary>
    /// <remarks>
    /// Values are normalized to snake_case when serialized into a tool schema.
    /// </remarks>
    public virtual ICollection<string> EnumValues { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<string> IFunctionParameter.EnumValues => (IReadOnlyCollection<string>)EnumValues;
}
