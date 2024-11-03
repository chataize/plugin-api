using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsButton : ISettingsButton
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public ButtonStyle Style { get; set; }

    public required Func<ValueTask> Callback { get; set; }
}
