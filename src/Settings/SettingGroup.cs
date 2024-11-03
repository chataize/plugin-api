using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingGroup : ISettingGroup
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required ICollection<IPluginSetting> Settings { get; set; }
}
