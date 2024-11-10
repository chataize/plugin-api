using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Utilities.Extensions;

namespace ChatAIze.PluginApi;

public class ChatFunction : IChatFunction
{
    public ChatFunction() { }

    [SetsRequiredMembers]
    public ChatFunction(Delegate callback)
    {
        Name = callback.GetNormalizedMethodName();
        Description = callback.Method.GetCustomAttribute<DescriptionAttribute>()?.Description;
        Callback = callback;
    }

    [SetsRequiredMembers]
    public ChatFunction(string name, string? description = null, Delegate? callback = null, ICollection<IFunctionParameter>? parameters = null)
    {
        Name = name;
        Description = description;
        Callback = callback;
        Parameters = parameters;
    }

    [SetsRequiredMembers]
    public ChatFunction(string name, string? description = null, Delegate? callback = null, params IFunctionParameter[] parameters)
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
