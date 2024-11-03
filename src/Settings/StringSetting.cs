using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class StringSetting : IStringSetting
{
    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? DefaultValue { get; set; }

    public virtual string? Placeholder { get; set; }

    public virtual int MaxLength { get; set; }

    public virtual int EditorLines { get; set; }

    public virtual bool IsSecret { get; set; }
}
