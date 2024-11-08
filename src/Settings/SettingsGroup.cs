using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsGroup : ISettingsGroup
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual required ICollection<IPluginSetting> Settings { get; set; }
}
