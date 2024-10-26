using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi;

public class ChatbotPlugin : IChatbotPlugin
{
    public virtual required Guid Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual string Version { get; set; } = "1.0.0";

    public virtual ICollection<IChatbotFunction> Functions { get; set; } = [];
}
