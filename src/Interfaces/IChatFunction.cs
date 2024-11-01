namespace ChatAIze.PluginApi.Interfaces;

public interface IChatFunction
{
    public string Name { get; }

    public string? Description => null;

    public ICollection<IFunctionParameter> Parameters => [];

    public ValueTask<object?> ExecuteAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken = default);
}
