using ChatAIze.PluginApi.Enums;

namespace ChatAIze.PluginApi.Interfaces;

public interface IFunctionParameter
{
    public string Name { get; }

    public string? Description => null;

    public ParameterType Type => ParameterType.Text;

    public ICollection<string> EnumValues => [];
}
