using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsParagraph : ISettingsParagraph
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required string Content { get; set; }
}
