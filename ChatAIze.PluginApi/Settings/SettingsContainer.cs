using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Basic <see cref="ISettingsContainer"/> implementation backed by a mutable <see cref="ISetting"/> collection.
/// </summary>
/// <remarks>
/// This is a general-purpose container used by plugin authors to assemble a settings tree.
/// In ChatAIze.Chatbot, nested containers are rendered by the settings UI (<see cref="ISettingsSection"/>, <see cref="ISettingsGroup"/>),
/// while leaf settings (for example <see cref="StringSetting"/>) are the ones that actually store values under their <see cref="ISetting.Id"/> keys.
/// </remarks>
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
