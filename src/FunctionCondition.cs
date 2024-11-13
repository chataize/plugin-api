using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi;

public class FunctionCondition : IFunctionCondition
{
    public FunctionCondition() { }

    [SetsRequiredMembers]
    public FunctionCondition(string key, string title, Func<IConditionContext, CancellationToken, ValueTask<(bool, string?)>> callback, params ICollection<ISetting>? settings)
    {
        Id = key;
        Title = title;
        Callback = callback;
        Settings = settings ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual bool IsPrecondition { get; set; }

    public virtual required Func<IConditionContext, CancellationToken, ValueTask<(bool, string?)>> Callback { get; set; }

    public virtual ICollection<ISetting> Settings { get; set; } = [];

    IReadOnlyCollection<ISetting> IFunctionCondition.Settings => (IReadOnlyCollection<ISetting>)Settings;

    public void AddSetting(ISetting setting)
    {
        Settings.Add(setting);
    }
}
