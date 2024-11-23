using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

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

public static class SelectionChoiceExtensions
{
    public static void AddChoice(this SelectionSetting setting, string value, string? title = null, bool isDisabled = false)
    {
        var choice = new SelectionChoice(value, title, isDisabled);
        setting.Choices.Add(choice);
    }
}
