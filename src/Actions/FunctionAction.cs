using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Actions;
using ChatAIze.Abstractions.Actions.Properties;

namespace ChatAIze.PluginApi.Actions;

public class FunctionAction : IFunctionAction
{
    public FunctionAction() { }

    [SetsRequiredMembers]
    public FunctionAction(string key, string title, Delegate? callback = null, ICollection<IActionProperty>? properties = null)
    {
        Id = key;
        Title = title;
        Callback = callback;
        Properties = properties ?? [];
    }

    [SetsRequiredMembers]
    public FunctionAction(string key, string title, Delegate? callback = null, params IActionProperty[] properties)
    {
        Id = key;
        Title = title;
        Callback = callback;
        Properties = properties ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual Delegate? Callback { get; set; }

    public virtual ICollection<IActionProperty> Properties { get; set; } = [];

    public void AddProperty(IActionProperty property)
    {
        Properties.Add(property);
    }
}
