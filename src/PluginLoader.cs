using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi;

public abstract class PluginLoader : IPluginLoader
{
    public abstract ValueTask<IChatbotPlugin> LoadAsync(IPluginSettings settings, CancellationToken cancellationToken = default);
}
