using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

public class FunctionParameter : IFunctionParameter
{
    public FunctionParameter() { }

    [SetsRequiredMembers]
    public FunctionParameter(Type type, string name, string? description = null, bool isRequired = false, ICollection<string>? enumValues = null)
    {
        Type = type;
        Name = name;
        Description = description;
        IsRequired = isRequired;
        EnumValues = enumValues ?? [];
    }

    public required virtual Type Type { get; set; }

    public required virtual string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual bool IsRequired { get; set; }

    public virtual ICollection<string> EnumValues { get; set; } = [];
}
