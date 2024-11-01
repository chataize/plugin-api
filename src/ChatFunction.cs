using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

public class ChatFunction : IChatFunction
{
    public ChatFunction() { }

    [SetsRequiredMembers]
    public ChatFunction(string name, string? description = null, ICollection<IFunctionParameter>? parameters = null, Delegate? callback = null)
    {
        Name = name;
        Description = description;
        Parameters = parameters ?? [];
        Callback = callback;
    }

    [SetsRequiredMembers]
    public ChatFunction(string name, Delegate? callback = null) : this(name, null, null, callback) { }

    [SetsRequiredMembers]
    public ChatFunction(string name, string? description = null, Delegate? callback = null) : this(name, description, null, callback) { }

    [SetsRequiredMembers]
    public ChatFunction(string name, ICollection<IFunctionParameter>? parameters = null, Delegate? callback = null) : this(name, null, parameters, callback) { }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual ICollection<IFunctionParameter> Parameters { get; set; } = [];

    public bool RequiresDoubleCheck { get; set; }

    public virtual Delegate? Callback { get; set; }
}
