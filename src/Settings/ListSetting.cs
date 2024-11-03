using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class ListSetting : IListSetting
{
    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual int MaxItems { get; set; }

    public virtual int MaxItemLength { get; set; }
}
