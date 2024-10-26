namespace ChatAIze.PluginApi.Interfaces;

public interface IChatbotPlugin
{
    public Guid Id { get; }

    public string Name { get; }

    public string? Description => null;

    public string Version => "1.0.0";

    public ICollection<IChatbotFunction> Functions { get; }
}
