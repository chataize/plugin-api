using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class BooleanSetting : IBooleanSetting
{
    public BooleanSetting() { }

    [SetsRequiredMembers]
    public BooleanSetting(string key, string title, bool defaultValue = false)
    {
        Key = key;
        Title = title;
        DefaultValue = defaultValue;
    }

    [SetsRequiredMembers]
    public BooleanSetting(string key, string title, BooleanSettingStyle style, bool defaultValue = false)
    {
        Key = key;
        Title = title;
        Style = style;
        DefaultValue = defaultValue;
    }

    [SetsRequiredMembers]
    public BooleanSetting(string key, string title, string? description, BooleanSettingStyle style = BooleanSettingStyle.ToggleSwitch, bool defaultValue = false)
    {
        Key = key;
        Title = title;
        Description = description;
        Style = style;
        DefaultValue = defaultValue;
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual BooleanSettingStyle Style { get; set; }

    public virtual bool DefaultValue { get; set; }
}
