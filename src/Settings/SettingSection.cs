using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingSection : ISettingsSection
{
    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ICollection<IPluginSetting> Settings { get; set; } = [];
}
