using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionChoice : ISelectionChoice
{
    public SelectionChoice() { }

    [SetsRequiredMembers]
    public SelectionChoice(string value, string? title = null, bool isDisabled = false)
    {
        Value = value;
        Title = title;
        IsDisabled = isDisabled;
    }

    public virtual string? Title { get; set; }

    public virtual required string Value { get; set; }

    public bool IsDisabled { get; set; }
}
