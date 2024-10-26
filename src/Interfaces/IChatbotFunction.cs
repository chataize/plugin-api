namespace ChatAIze.PluginApi.Interfaces;

public interface IChatbotFunction
{
    public string Name { get; }

    public string? Description { get; }

    public ICollection<IFunctionParameter> Parameters => [];

    public ValueTask<object?> ExecuteAsync();
}
