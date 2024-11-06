using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsSection : ISettingsSection
{
    public SettingsSection() { }

    [SetsRequiredMembers]
    public SettingsSection(string title, ICollection<IPluginSetting>? settings = null)
    {
        Title = title;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public SettingsSection(string title, params IPluginSetting[] settings)
    {
        Title = title;
        Settings = settings;
    }

    [SetsRequiredMembers]
    public SettingsSection(string title, string? description, ICollection<IPluginSetting>? settings = null)
    {
        Title = title;
        Description = description;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public SettingsSection(string title, string? description, params IPluginSetting[] settings)
    {
        Title = title;
        Description = description;
        Settings = settings;
    }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ICollection<IPluginSetting> Settings { get; set; } = [];
}
