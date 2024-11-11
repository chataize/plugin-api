using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Actions;

namespace ChatAIze.PluginApi.Settings;

public class ListSetting : IListSetting
{
    public ListSetting() { }

    [SetsRequiredMembers]
    public ListSetting(string key, string? title = null, string? description = null, string? itemPlaceholder = null, int maxItems = 100, int maxItemLength = 100, bool isDisabled = false)
    {
        Key = key;
        Title = title;
        Description = description;
        ItemPlaceholder = itemPlaceholder;
        MaxItems = maxItems;
        MaxItemLength = maxItemLength;
        IsDisabled = isDisabled;
    }

    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? ItemPlaceholder { get; set; }

    public virtual int MaxItems { get; set; } = 100;

    public virtual int MaxItemLength { get; set; } = 100;

    public bool IsDisabled { get; set; }
}

public static class ListSettingExtensions
{
    public static void AddListSetting(this ChatbotPlugin plugin, string key, string? title = null, string? description = null, string? itemPlaceholder = null, int maxItems = 100, int maxItemLength = 100, bool isDisabled = false)
    {
        var setting = new ListSetting(key, title, description, itemPlaceholder, maxItems, maxItemLength, isDisabled);
        plugin.Settings.Add(setting);
    }

    public static void AddListSetting(this FunctionAction action, string key, string? title = null, string? description = null, string? itemPlaceholder = null, int maxItems = 100, int maxItemLength = 100, bool isDisabled = false)
    {
        var setting = new ListSetting(key, title, description, itemPlaceholder, maxItems, maxItemLength, isDisabled);
        action.Settings.Add(setting);
    }
}
