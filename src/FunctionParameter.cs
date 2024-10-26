using ChatAIze.PluginApi.Enums;
using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi;

public class FunctionParameter : IFunctionParameter
{
    public required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual ParameterType Type { get; set; }
}
