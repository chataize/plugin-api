using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingGroup : ISettingGroup
{
    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual required ICollection<IPluginSetting> Settings { get; set; }
}
