using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class SelectionSetting : ISelectionSetting
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual SelectionSettingStyle Style { get; set; }

    public required virtual string DefaultValue { get; set; }

    public virtual ICollection<ISelectionChoice> Choices { get; set; } = [];

    public virtual bool IsCompact { get; set; }

    public virtual bool IsDisabled { get; set; }
}
