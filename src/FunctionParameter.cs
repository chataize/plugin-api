using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

public class FunctionParameter : IFunctionParameter
{
    public FunctionParameter() { }

    [SetsRequiredMembers]
    public FunctionParameter(string name, string? description, Type type)
    {
        Name = name;
        Description = description;
        Type = type;
    }

    [SetsRequiredMembers]
    public FunctionParameter(string name, Type type, string? description = null) : this(name, description, type) { }

    [SetsRequiredMembers]
    public FunctionParameter(Type type, string name, string? description = null) : this(name, description, type) { }

    public required virtual string Name { get; set; }

    public virtual string? Description { get; set; }

    public required virtual Type Type { get; set; }

    public virtual ICollection<string> EnumValues { get; set; } = [];

    public virtual bool IsRequired { get; set; }
}
