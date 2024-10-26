using System.Diagnostics.CodeAnalysis;
using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi;

public class ChatbotFunction : IChatbotFunction
{
    public ChatbotFunction() { }

    [SetsRequiredMembers]
    public ChatbotFunction(string name, string? description = null, ICollection<IFunctionParameter>? parameters = null, Func<ValueTask<object?>>? callback = null)
    {
        Name = name;
        Description = description;
        Parameters = parameters ?? [];
        Callback = callback;
    }

    [SetsRequiredMembers]
    public ChatbotFunction(string name, Func<ValueTask<object?>>? callback = null) : this(name, null, null, callback) { }

    [SetsRequiredMembers]
    public ChatbotFunction(string name, string? description = null, Func<ValueTask<object?>>? callback = null) : this(name, description, null, callback) { }

    [SetsRequiredMembers]
    public ChatbotFunction(string name, ICollection<IFunctionParameter>? parameters = null, Func<ValueTask<object?>>? callback = null) : this(name, null, parameters, callback) { }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual ICollection<IFunctionParameter> Parameters { get; set; } = [];

    public virtual Func<ValueTask<object?>>? Callback { get; set; }

    public virtual ValueTask<object?> ExecuteAsync()
    {
        if (Callback is null)
        {
            throw new InvalidOperationException("Callback is not set.");
        }

        return Callback();
    }
}
