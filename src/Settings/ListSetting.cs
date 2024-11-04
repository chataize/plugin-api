using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class ListSetting : IListSetting
{
    public ListSetting() { }

    [SetsRequiredMembers]
    public ListSetting(string key, string title, int maxItems = 100, int maxItemLength = 100)
    {
        Key = key;
        Title = title;
        MaxItems = maxItems;
        MaxItemLength = maxItemLength;
    }

    [SetsRequiredMembers]
    public ListSetting(string key, string title, string? description, int maxItems = 100, int maxItemLength = 100)
    {
        Key = key;
        Title = title;
        Description = description;
        MaxItems = maxItems;
        MaxItemLength = maxItemLength;
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual int MaxItems { get; set; } = 100;

    public virtual int MaxItemLength { get; set; } = 100;
}
