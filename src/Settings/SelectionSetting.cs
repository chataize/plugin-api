using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting : ISelectionSetting
{
    public required string Key { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public SelectionSettingStyle Style { get; set; }

    public required ICollection<ISelectionChoice> Choices { get; set; }
}
