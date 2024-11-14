using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class ListSetting : IListSetting
{
    public ListSetting() { }

    [SetsRequiredMembers]
    public ListSetting(string id, string? title = null, string? description = null, string? itemPlaceholder = null, int maxItems = 100, int maxItemLength = 100, bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        ItemPlaceholder = itemPlaceholder;
        MaxItems = maxItems;
        MaxItemLength = maxItemLength;
        IsDisabled = isDisabled;
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? ItemPlaceholder { get; set; }

    public virtual int MaxItems { get; set; } = 100;

    public virtual int MaxItemLength { get; set; } = 100;

    public bool IsDisabled { get; set; }
}

public static class ListSettingExtensions
{
    public static void AddListSetting(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, string? itemPlaceholder = null, int maxItems = 100, int maxItemLength = 100, bool isDisabled = false)
    {
        var setting = new ListSetting(id, title, description, itemPlaceholder, maxItems, maxItemLength, isDisabled);
        container.Settings.Add(setting);
    }
}
