namespace ChatAIze.PluginApi.Interfaces;

public interface IPluginLoader
{
    public ValueTask<IChatbotPlugin> LoadAsync();
}
