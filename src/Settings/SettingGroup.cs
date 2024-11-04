using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingGroup : ISettingsGroup
{
    public SettingGroup() { }

    [SetsRequiredMembers]
    public SettingGroup(string title, ICollection<IPluginSetting>? settings = null)
    {
        Title = title;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public SettingGroup(string title, string? description, ICollection<IPluginSetting>? settings = null)
    {
        Title = title;
        Description = description;
        Settings = settings ?? [];
    }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual required ICollection<IPluginSetting> Settings { get; set; }
}
