using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

public class ChatFunction : IStoredChatFunction
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

    public virtual required Guid Id { get; set; }

    public virtual required string Name { get; set; }

    public virtual string? Description { get; set; }

    public virtual ICollection<IFunctionParameter> Parameters { get; set; } = [];

    public virtual bool IsEnabled { get; set; } = true;

    public virtual bool RequiresVerifiedEmail { get; set; }

    public virtual bool RequiresDoubleCheck { get; set; }

    public virtual bool RequiresConfirmation { get; set; }

    public virtual string? ConfirmationTitle { get; set; }

    public virtual string? ConfirmationMessage { get; set; }

    public virtual string? ConfirmationYesText { get; set; }

    public virtual string? ConfirmationNoText { get; set; }

    public virtual int PersonalDailyLimit { get; set; }

    public virtual int SharedDailyLimit { get; set; }

    public virtual Delegate? Callback { get; set; }
}
