using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class ListSetting : IListSetting
{
    public ListSetting() { }

    [SetsRequiredMembers]
    public ListSetting(string key, string title, string? description = null, string? itemPlaceholder = null, int maxItems = 100, int maxItemLength = 100)
    {
        Key = key;
        Title = title;
        Description = description;
        ItemPlaceholder = itemPlaceholder;
        MaxItems = maxItems;
        MaxItemLength = maxItemLength;
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? ItemPlaceholder { get; set; }

    public virtual int MaxItems { get; set; } = 100;

    public virtual int MaxItemLength { get; set; } = 100;
}
