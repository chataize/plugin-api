using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class IntegerSetting : IIntegerSetting
{
    public required string Key { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public IntegerSettingStyle Style { get; set; }

    public int DefaultValue { get; set; }

    public int MinValue { get; set; }

    public int MaxValue { get; set; }
}
