using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public interface IEditableSettingsContainer
{
    public ICollection<ISetting> Settings { get; }
}
