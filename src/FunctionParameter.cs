using System.Diagnostics.CodeAnalysis;
using ChatAIze.PluginApi.Enums;
using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi;

public class FunctionParameter : IFunctionParameter
{
    public FunctionParameter() { }

    [SetsRequiredMembers]
    public FunctionParameter(string name, string? description = null, ParameterType type = ParameterType.Text)
    {
        Name = name;
        Description = description;
        Type = type;
    }

    [SetsRequiredMembers]
    public FunctionParameter(string name, ParameterType type = ParameterType.Text, string? description = null) : this(name, description, type) { }

    [SetsRequiredMembers]
    public FunctionParameter(ParameterType type, string name, string? description = null) : this(name, description, type) { }

    public required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual ParameterType Type { get; set; }
}
