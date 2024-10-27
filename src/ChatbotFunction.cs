using System.Diagnostics.CodeAnalysis;
using ChatAIze.PluginApi.Interfaces;

using CallbackT = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.ValueTask<object?>>;

namespace ChatAIze.PluginApi;

public class ChatbotFunction : IChatbotFunction
{
    public ChatbotFunction() { }

    [SetsRequiredMembers]
    public ChatbotFunction(string name, string? description = null, ICollection<IFunctionParameter>? parameters = null, CallbackT? callback = null)
    {
        Name = name;
        Description = description;
        Parameters = parameters ?? [];
        Callback = callback;
    }

    [SetsRequiredMembers]
    public ChatbotFunction(string name, CallbackT? callback = null) : this(name, null, null, callback) { }

    [SetsRequiredMembers]
    public ChatbotFunction(string name, string? description = null, CallbackT? callback = null) : this(name, description, null, callback) { }

    [SetsRequiredMembers]
    public ChatbotFunction(string name, ICollection<IFunctionParameter>? parameters = null, CallbackT? callback = null) : this(name, null, parameters, callback) { }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual ICollection<IFunctionParameter> Parameters { get; set; } = [];

    public virtual CallbackT? Callback { get; set; }

    public virtual ValueTask<object?> ExecuteAsync(IDictionary<string, object> parameters)
    {
        if (Callback is null)
        {
            throw new InvalidOperationException("Callback is not set.");
        }

        return Callback(parameters);
    }
}
