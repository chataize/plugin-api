using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

public class ChatFunction : IChatFunction
{
    public ChatFunction() { }

    [SetsRequiredMembers]
    public ChatFunction(string name, string? description = null, Delegate? callback = null, ICollection<IFunctionParameter>? parameters = null)
    {
        Name = name;
        Description = description;
        Callback = callback;
        Parameters = parameters;
    }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual bool RequiresDoubleCheck { get; set; }

    public virtual Delegate? Callback { get; set; }

    public virtual ICollection<IFunctionParameter>? Parameters { get; set; }
}
