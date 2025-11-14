using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Represents a container that allows modification of a collection of settings.
/// </summary>
/// <remarks>
/// This interface is typically used for programmatically constructing or extending plugin settings at runtime.
/// </remarks>
public interface IEditableSettingsContainer
{
    /// <summary>
    /// Gets the mutable collection of settings in this container.
    /// </summary>
    public ICollection<ISetting> Settings { get; }
}
