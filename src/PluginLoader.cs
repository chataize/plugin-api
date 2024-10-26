using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi;

public abstract class PluginLoader : IPluginLoader
{
    public abstract ValueTask<IChatbotPlugin> LoadAsync();
}
