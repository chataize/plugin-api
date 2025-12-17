using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

/// <summary>
/// Minimal abstraction for a settings container with a mutable <see cref="ISetting"/> collection.
/// </summary>
/// <remarks>
/// This interface exists primarily to support fluent builder-style extension methods (for example <see cref="StringSettingExtensions"/>).
/// It is implemented by common types such as <see cref="ChatAIze.PluginApi.ChatbotPlugin"/>,
/// <see cref="ChatAIze.PluginApi.FunctionAction"/>, <see cref="ChatAIze.PluginApi.FunctionCondition"/>,
/// and the various container settings (<see cref="SettingsSection"/>, <see cref="SettingsGroup"/>).
/// <para>
/// In ChatAIze.Chatbot, leaf settings (for example <see cref="StringSetting"/>) store values under their <see cref="ISetting.Id"/> keys.
/// Container settings (sections/groups/paragraphs) are used for layout and typically do not store values.
/// </para>
/// </remarks>
public interface IEditableSettingsContainer
{
    /// <summary>
    /// Gets the mutable collection of settings in this container.
    /// </summary>
    public ICollection<ISetting> Settings { get; }
}
