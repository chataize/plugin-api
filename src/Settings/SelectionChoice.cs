using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionChoice : ISelectionChoice
{
    public SelectionChoice() { }

    [SetsRequiredMembers]
    public SelectionChoice(string label, string value)
    {
        Label = label;
        Value = value;
    }

    public virtual required string Label { get; set; }

    public virtual required string Value { get; set; }
}
