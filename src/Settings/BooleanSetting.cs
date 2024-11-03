using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class BooleanSetting : IBooleanSetting
{
    public required string Key { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public BooleanSettingStyle Style { get; set; }

    public bool DefaultValue { get; set; }
}
