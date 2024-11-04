using ChatAIze.Abstractions.Plugins;

namespace ChatAIze.PluginApi;

public abstract class PluginLoader : IPluginLoader
{
    public abstract ValueTask<IChatbotPlugin> LoadAsync(CancellationToken cancellationToken = default);
}
