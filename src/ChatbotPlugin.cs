using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

public class ChatbotPlugin : IChatbotPlugin, IEditableSettingsContainer
{
    public ChatbotPlugin()
    {
        SettingsCallback ??= (_, _) => ValueTask.FromResult((IReadOnlyCollection<ISetting>)Settings);
        FunctionsCallback ??= (_, _) => ValueTask.FromResult((IReadOnlyCollection<IChatFunction>)Functions);
        ActionsCallback ??= (_, _) => ValueTask.FromResult((IReadOnlyCollection<IFunctionAction>)Actions);
        ConditionsCallback ??= (_, _) => ValueTask.FromResult((IReadOnlyCollection<IFunctionCondition>)Conditions);
    }

    [SetsRequiredMembers]
    public ChatbotPlugin(string id, string title, string? description = null, string? website = null, string? author = null, Version? version = null, DateTimeOffset? releaseTime = null, DateTimeOffset? lastUpdateTime = null, ICollection<ISetting>? settings = null, ICollection<IChatFunction>? functions = null, ICollection<IFunctionAction>? actions = null, ICollection<IFunctionCondition>? conditions = null, Func<IPluginSettings, CancellationToken, ValueTask<IReadOnlyCollection<ISetting>>>? settingsCallback = null, Func<IPluginSettings, CancellationToken, ValueTask<IReadOnlyCollection<IChatFunction>>>? functionsCallback = null, Func<IPluginSettings, CancellationToken, ValueTask<IReadOnlyCollection<IFunctionAction>>>? actionsCallback = null, Func<IPluginSettings, CancellationToken, ValueTask<IReadOnlyCollection<IFunctionCondition>>>? conditionsCallback = null)
    {
        Id = id;
        Title = title;
        Description = description;
        Website = website;
        Author = author;
        Version = version ?? new Version(1, 0, 0, 0);
        ReleaseTime = releaseTime;
        LastUpdateTime = lastUpdateTime;
        Settings = settings ?? [];
        Functions = functions ?? [];
        Actions = actions ?? [];
        Conditions = conditions ?? [];

        if (settingsCallback is null)
        {
            SettingsCallback = (_, _) => ValueTask.FromResult((IReadOnlyCollection<ISetting>)Settings);
        }
        else
        {
            SettingsCallback = settingsCallback;
        }

        if (functionsCallback is null)
        {
            FunctionsCallback = (_, _) => ValueTask.FromResult((IReadOnlyCollection<IChatFunction>)Functions);
        }
        else
        {
            FunctionsCallback = functionsCallback;
        }

        if (actionsCallback is null)
        {
            ActionsCallback = (_, _) => ValueTask.FromResult((IReadOnlyCollection<IFunctionAction>)Actions);
        }
        else
        {
            ActionsCallback = actionsCallback;
        }

        if (conditionsCallback is null)
        {
            ConditionsCallback = (_, _) => ValueTask.FromResult((IReadOnlyCollection<IFunctionCondition>)Conditions);
        }
        else
        {
            ConditionsCallback = conditionsCallback;
        }
    }

    public virtual required string Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? Website { get; set; }

    public virtual string? Author { get; set; }

    public virtual Version Version { get; set; } = new Version(1, 0, 0, 0);

    public virtual DateTimeOffset? ReleaseTime { get; set; }

    public virtual DateTimeOffset? LastUpdateTime { get; set; }

    public virtual ICollection<ISetting> Settings { get; set; } = [];

    public virtual ICollection<IChatFunction> Functions { get; set; } = [];

    public virtual ICollection<IFunctionAction> Actions { get; set; } = [];

    public virtual ICollection<IFunctionCondition> Conditions { get; set; } = [];

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<IReadOnlyCollection<ISetting>>>? SettingsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<IReadOnlyCollection<IChatFunction>>>? FunctionsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<IReadOnlyCollection<IFunctionAction>>>? ActionsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<IReadOnlyCollection<IFunctionCondition>>>? ConditionsCallback { get; set; }

    public virtual void AddSetttng(ISetting setting)
    {
        Settings.Add(setting);
    }

    public virtual void AddFunction(IChatFunction function)
    {
        Functions.Add(function);
    }

    public virtual void AddFunction(Delegate function)
    {
        Functions.Add(new ChatFunction(function));
    }

    public virtual void AddAction(IFunctionAction action)
    {
        Actions.Add(action);
    }

    public virtual void AddCondition(IFunctionCondition condition)
    {
        Conditions.Add(condition);
    }
}
