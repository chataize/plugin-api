using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsSection : ISettingsSection, IEditableSettingsContainer
{
    public SettingsSection() { }

    [SetsRequiredMembers]
    public SettingsSection(string id, string? title = null, string? description = null, bool isDisabled = false, params ICollection<ISetting>? settings)
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

public static class SettingsSectionExtensions
{
    public static void AddSettingsSection(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, bool isDisabled = false, params ICollection<ISetting>? settings)
    {
        var section = new SettingsSection(id, title, description, isDisabled, settings);
        container.Settings.Add(section);
    }
}
