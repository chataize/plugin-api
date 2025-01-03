using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsGroup : ISettingsGroup, IEditableSettingsContainer
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
    public static IEditableSettingsContainer AddSettingsGroup(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, bool isDisabled = false, params ICollection<ISetting>? settings)
    {
        var setting = new SettingsGroup(id, title, description, isDisabled, settings);
        container.Settings.Add(setting);

        return container;
    }

    public static ICollection<ISetting> AddSettingsGroup(this ICollection<ISetting> collection, string id, string? title = null, string? description = null, bool isDisabled = false, params ICollection<ISetting>? settings)
    {
        var setting = new SettingsGroup(id, title, description, isDisabled, settings);
        collection.Add(setting);

        return collection;
    }
}
