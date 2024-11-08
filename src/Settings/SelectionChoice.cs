using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionChoice : ISelectionChoice
{
    public virtual string? Title { get; set; }

    public virtual required string Value { get; set; }

    public bool IsDisabled { get; set; }
}
