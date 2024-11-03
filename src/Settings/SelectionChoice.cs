using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionChoice : ISelectionChoice
{
    public virtual required string Label { get; set; }

    public virtual required string Value { get; set; }
}
