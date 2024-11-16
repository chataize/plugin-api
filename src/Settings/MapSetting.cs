using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class MapSetting : IMapSetting
{
    public MapSetting() { }

    [SetsRequiredMembers]
    public MapSetting(string id, string? title = null, string? description = null, string? keyPlaceholder = null, string? valuePlaceholder = null, int maxItems = 100, int maxKeyLength = 100, int maxValueLength = 100, bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        KeyPlaceholder = keyPlaceholder;
        ValuePlaceholder = valuePlaceholder;
        MaxItems = maxItems;
        MaxKeyLength = maxKeyLength;
        MaxValueLength = maxValueLength;
        IsDisabled = isDisabled;
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? KeyPlaceholder { get; set; }

    public virtual string? ValuePlaceholder { get; set; }

    public virtual int MaxItems { get; set; } = 100;

    public virtual int MaxKeyLength { get; set; } = 100;

    public virtual int MaxValueLength { get; set; } = 100;

    public virtual object DefaultValueObject => new Dictionary<string, string>();

    public bool IsDisabled { get; set; }
}

public static class MapSettingExtensions
{
    public static void AddMapSetting(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, string? keyPlaceholder = null, string? valuePlaceholder = null, int maxItems = 100, int maxKeyLength = 100, int maxValueLength = 100, bool isDisabled = false)
    {
        var setting = new MapSetting(id, title, description, keyPlaceholder, valuePlaceholder, maxItems, maxKeyLength, maxValueLength, isDisabled);
        container.Settings.Add(setting);
    }
}
