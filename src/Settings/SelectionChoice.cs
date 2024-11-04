using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SelectionChoice<T> : ISelectionChoice<T>
{
    public SelectionChoice() { }

    [SetsRequiredMembers]
    public SelectionChoice(string label, T value)
    {
        Label = label;
        Value = value;
    }

    public virtual required string Label { get; set; }

    public virtual required T Value { get; set; }
}
