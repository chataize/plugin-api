using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class MapSetting : IMapSetting
{
    public virtual required string Key { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? KeyPlaceholder { get; set; }

    public virtual string? ValuePlaceholder { get; set; }

    public virtual int MaxItems { get; set; } = 100;

    public virtual int MaxKeyLength { get; set; } = 100;

    public virtual int MaxValueLength { get; set; } = 100;

    public bool IsDisabled { get; set; }
}
