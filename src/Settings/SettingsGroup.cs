using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsGroup : ISettingsGroup
{
    public SettingsGroup() { }

    [SetsRequiredMembers]
    public SettingsGroup(string key, string? title = null, string? description = null, bool isDisabled = false, ICollection<IPluginSetting>? settings = null)
    {
        Key = key;
        Title = title;
        Description = description;
        IsDisabled = isDisabled;
        Settings = settings ?? [];
    }

    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual ICollection<IPluginSetting> Settings { get; set; } = [];
}
