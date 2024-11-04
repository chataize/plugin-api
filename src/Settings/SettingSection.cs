using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingSection : ISettingsSection
{
    public SettingSection() { }

    [SetsRequiredMembers]
    public SettingSection(string title, ICollection<IPluginSetting>? settings = null)
    {
        Title = title;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public SettingSection(string title, string? description, ICollection<IPluginSetting>? settings = null)
    {
        Title = title;
        Description = description;
        Settings = settings ?? [];
    }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual ICollection<IPluginSetting> Settings { get; set; } = [];
}
