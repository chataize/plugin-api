using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingSection : ISettingSection
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public ICollection<IPluginSetting> Settings { get; set; } = [];
}
