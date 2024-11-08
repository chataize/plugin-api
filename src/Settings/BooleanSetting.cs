using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class BooleanSetting : IBooleanSetting
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual BooleanSettingStyle Style { get; set; }

    public virtual bool DefaultValue { get; set; }

    public virtual bool IsCompact { get; set; }

    public bool IsDisabled { get; set; }
}
