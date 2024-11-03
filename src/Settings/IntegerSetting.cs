using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class IntegerSetting : IIntegerSetting
{
    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual IntegerSettingStyle Style { get; set; }

    public virtual int DefaultValue { get; set; }

    public virtual int MinValue { get; set; }

    public virtual int MaxValue { get; set; }
}
