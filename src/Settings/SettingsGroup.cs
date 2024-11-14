using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsGroup : ISettingsGroup
{
    public SettingsGroup() { }

    [SetsRequiredMembers]
    public SettingsGroup(string id, string? title = null, string? description = null, bool isDisabled = false, params ICollection<ISetting>? settings)
    {
        Id = id;
        Title = title;
        Description = description;
        IsDisabled = isDisabled;
        Settings = settings ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual bool IsDisabled { get; set; }

    public virtual ICollection<ISetting> Settings { get; set; } = [];

    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;
}

public static class SettingsGroupExtensions
{
    public static void AddSettingsGroup(this ChatbotPlugin plugin, string id, string? title = null, string? description = null, bool isDisabled = false, params ICollection<ISetting>? settings)
    {
        var group = new SettingsGroup(id, title, description, isDisabled, settings);
        plugin.Settings.Add(group);
    }

    public static void AddSettingsGroup(this ChatbotPlugin plugin, string id, string? title = null, string? description = null, bool isDisabled = false)
    {
        var group = new SettingsGroup(id, title, description, isDisabled);
        plugin.Settings.Add(group);
    }

    public static void AddSettingsGroup(this FunctionAction action, string id, string? title = null, string? description = null, bool isDisabled = false, params ICollection<ISetting>? settings)
    {
        var group = new SettingsGroup(id, title, description, isDisabled, settings);
        action.Settings.Add(group);
    }
}
