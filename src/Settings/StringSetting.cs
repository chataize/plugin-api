using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class StringSetting : IStringSetting
{
    public required string Key { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public string? DefaultValue { get; set; }

    public string? Placeholder { get; set; }

    public int MaxLength { get; set; }

    public int EditorLines { get; set; }

    public bool IsSecret { get; set; }
}
