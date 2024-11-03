using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsParagraph : ISettingsParagraph
{
    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual required string Content { get; set; }
}
