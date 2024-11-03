using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class ListSetting : IListSetting
{
    public required string Key { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public int MaxItems { get; set; }

    public int MaxItemLength { get; set; }
}
