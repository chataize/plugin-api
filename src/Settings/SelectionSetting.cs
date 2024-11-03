using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting : ISelectionSetting
{
    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual SelectionSettingStyle Style { get; set; }

    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];
}
