using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsParagraph : ISettingsParagraph
{
    public virtual required string Key { get; set; }

    public virtual string? Content { get; set; }

    public virtual bool IsDisabled { get; set; }
}
