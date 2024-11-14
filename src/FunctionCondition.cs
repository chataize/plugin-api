using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi;

public class FunctionCondition : IFunctionCondition
{
    public FunctionCondition() { }

    [SetsRequiredMembers]
    public FunctionCondition(string id, string title, Delegate callback, params ICollection<ISetting>? settings)
    {
        Id = id;
        Title = title;
        Callback = callback;
        Settings = settings ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual bool IsPrecondition { get; set; }

    public virtual required Delegate Callback { get; set; }

    public virtual ICollection<ISetting> Settings { get; set; } = [];

    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;

    public void AddSetting(ISetting setting)
    {
        Settings.Add(setting);
    }
}