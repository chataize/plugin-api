using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi;

public class ChatbotFunction : IChatbotFunction
{
    public virtual required Guid Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual Func<ValueTask<object?>>? Callback { get; set; }

    public virtual ICollection<IFunctionParameter> Parameters { get; set; } = [];

    public virtual ValueTask<object?> ExecuteAsync()
    {
        if (Callback is null)
        {
            throw new InvalidOperationException("Callback is not set.");
        }

        return Callback();
    }
}
