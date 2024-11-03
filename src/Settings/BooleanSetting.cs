using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class BooleanSetting : IBooleanSetting
{
    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual BooleanSettingStyle Style { get; set; }

    public virtual bool DefaultValue { get; set; }
}
