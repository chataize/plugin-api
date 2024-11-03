using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionChoice : ISelectionChoice
{
    public required string Label { get; set; }

    public required string Value { get; set; }
}
