using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class BooleanSetting : IBooleanSetting
{
    public BooleanSetting() { }

    [SetsRequiredMembers]
    public BooleanSetting(string id, string? title = null, string? description = null, BooleanSettingStyle style = BooleanSettingStyle.ToggleSwitch, bool defaultValue = false, bool isCompact = false, bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
        IsCompact = isCompact;
        IsDisabled = isDisabled;
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual BooleanSettingStyle Style { get; set; }

    public virtual bool DefaultValue { get; set; }

    public virtual object DefaultValueObject => DefaultValue;

    public virtual bool IsCompact { get; set; }

    public bool IsDisabled { get; set; }
}

public static class BooleanSettingExtensions
{
    public static void AddBooleanSetting(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, BooleanSettingStyle style = BooleanSettingStyle.ToggleSwitch, bool defaultValue = false, bool isCompact = false, bool isDisabled = false)
    {
        var setting = new BooleanSetting(id, title, description, style, defaultValue, isCompact, isDisabled);
        container.Settings.Add(setting);
    }
}
