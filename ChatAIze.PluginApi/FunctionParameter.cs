using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

/// <inheritdoc cref="IFunctionParameter"/>
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
    /// <param name="enumValues">A list of allowed values if the parameter is an enumeration.</param>
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
    public virtual required string Name { get; set; }

    /// <inheritdoc />
    public virtual string? Description { get; set; }

    /// <inheritdoc />
    public virtual bool IsRequired { get; set; }

    /// <summary>
    /// Gets or sets the list of allowed enumeration values.
    /// </summary>
    public virtual ICollection<string> EnumValues { get; set; } = [];

    /// <inheritdoc />
    IReadOnlyCollection<string> IFunctionParameter.EnumValues => (IReadOnlyCollection<string>)EnumValues;
}
