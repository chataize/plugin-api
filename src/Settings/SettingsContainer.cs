using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a container that holds a collection of configurable settings.
/// </summary>
public class SettingsContainer : ISettingsContainer, IEditableSettingsContainer
{
    /// <summary>
    /// Gets or sets the modifiable collection of settings.
    /// </summary>
    public virtual ICollection<ISetting> Settings { get; set; } = [];

    /// <summary>
    /// Gets a read-only view of the settings collection.
    /// </summary>
    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;
}
