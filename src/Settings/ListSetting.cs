using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class ListSetting : IListSetting
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? ItemPlaceholder { get; set; }

    public virtual int MaxItems { get; set; } = 100;

    public virtual int MaxItemLength { get; set; } = 100;

    public bool IsDisabled { get; set; }
}
