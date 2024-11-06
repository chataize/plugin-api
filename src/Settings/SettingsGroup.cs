using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsGroup : ISettingsGroup
{
    public SettingsGroup() { }

    [SetsRequiredMembers]
    public SettingsGroup(string title, ICollection<IPluginSetting>? settings = null)
    {
        Title = title;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public SettingsGroup(string title, params IPluginSetting[] settings)
    {
        Title = title;
        Settings = settings;
    }

    [SetsRequiredMembers]
    public SettingsGroup(string title, string? description, ICollection<IPluginSetting>? settings = null)
    {
        Title = title;
        Description = description;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public SettingsGroup(string title, string? description, params IPluginSetting[] settings)
    {
        Title = title;
        Description = description;
        Settings = settings;
    }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual required ICollection<IPluginSetting> Settings { get; set; }
}
