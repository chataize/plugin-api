using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class ListSetting : IListSetting
{
    public ListSetting() { }

    [SetsRequiredMembers]
    public ListSetting(string id, string? title = null, string? description = null, string? itemPlaceholder = null, TextFieldType textFieldType = TextFieldType.Default, int maxItems = 100, int maxItemLength = 100, bool allowDuplicates = false, bool isLowercase = false, bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        ItemPlaceholder = itemPlaceholder;
        TextFieldType = textFieldType;
        MaxItems = maxItems;
        MaxItemLength = maxItemLength;
        AllowDuplicates = allowDuplicates;
        IsLowercase = isLowercase;
        IsDisabled = isDisabled;
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? ItemPlaceholder { get; set; }

    public virtual TextFieldType TextFieldType { get; set; }

    public virtual int MaxItems { get; set; } = 100;

    public virtual int MaxItemLength { get; set; } = 100;

    public virtual object DefaultValueObject => new List<string>();

    public virtual bool AllowDuplicates { get; set; }

    public virtual bool IsLowercase { get; set; }

    public virtual bool IsDisabled { get; set; }
}

public static class ListSettingExtensions
{
    public static IEditableSettingsContainer AddListSetting(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, string? itemPlaceholder = null, TextFieldType textFieldType = TextFieldType.Default, int maxItems = 100, int maxItemLength = 100, bool allowDuplicates = false, bool isLowercase = false, bool isDisabled = false)
    {
        var setting = new ListSetting(id, title, description, itemPlaceholder, textFieldType, maxItems, maxItemLength, allowDuplicates, isLowercase, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    public static ICollection<ISetting> AddListSetting(this ICollection<ISetting> collection, string id, string? title = null, string? description = null, string? itemPlaceholder = null, TextFieldType textFieldType = TextFieldType.Default, int maxItems = 100, int maxItemLength = 100, bool allowDuplicates = false, bool isLowercase = false, bool isDisabled = false)
    {
        var setting = new ListSetting(id, title, description, itemPlaceholder, textFieldType, maxItems, maxItemLength, allowDuplicates, isLowercase, isDisabled);
        collection.Add(setting);

        return collection;
    }
}
