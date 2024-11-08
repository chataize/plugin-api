using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting : ISelectionSetting
{
    public SelectionSetting() { }

    [SetsRequiredMembers]
    public SelectionSetting(string key, string? title = null, string? description = null, SelectionSettingStyle style = SelectionSettingStyle.Automatic, string? defaultValue = null, bool isCompact = false, bool isDisabled = false)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
    }

    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual SelectionSettingStyle Style { get; set; }

    public virtual string? DefaultValue { get; set; }

    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];

    public virtual bool IsCompact { get; set; }

    public virtual bool IsDisabled { get; set; }
}
