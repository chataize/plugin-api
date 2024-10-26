namespace ChatAIze.PluginApi.Interfaces;

public interface IChatbotFunction
{
    public Guid Id { get; }

    public string Name { get; }

    public string? Description { get; }

    public ICollection<IFunctionParameter> Parameters => [];

    public ValueTask<object?> ExecuteAsync();
}
