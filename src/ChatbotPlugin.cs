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
        SettingsCallback ??= (_, _) => (IReadOnlyCollection<ISetting>)Settings;
        FunctionsCallback ??= (_, _) => (IReadOnlyCollection<IChatFunction>)Functions;
        ActionsCallback ??= (_, _) => (IReadOnlyCollection<IFunctionAction>)Actions;
        ConditionsCallback ??= (_, _) => (IReadOnlyCollection<IFunctionCondition>)Conditions;
    }

    [SetsRequiredMembers]
    public ChatbotPlugin(string id, string title, string? description = null, string? iconUrl = null, string? website = null, string? author = null, Version? version = null, DateTimeOffset? releaseTime = null, DateTimeOffset? lastUpdateTime = null, ICollection<ISetting>? settings = null, ICollection<IChatFunction>? functions = null, ICollection<IFunctionAction>? actions = null, ICollection<IFunctionCondition>? conditions = null) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        IconUrl = iconUrl;
        Website = website;
        Author = author;
        Version = version ?? new Version(1, 0, 0, 0);
        ReleaseTime = releaseTime;
        LastUpdateTime = lastUpdateTime;
        Settings = settings ?? [];
        Functions = functions ?? [];
        Actions = actions ?? [];
        Conditions = conditions ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? IconUrl { get; set; }

    public virtual string? Website { get; set; }

    public virtual string? Author { get; set; }

    public virtual Version Version { get; set; } = new Version(1, 0, 0, 0);

    public virtual DateTimeOffset? ReleaseTime { get; set; }

    public virtual DateTimeOffset? LastUpdateTime { get; set; }

    public virtual ICollection<ISetting> Settings { get; set; } = [];

    public virtual ICollection<IChatFunction> Functions { get; set; } = [];

    public virtual ICollection<IFunctionAction> Actions { get; set; } = [];

    public virtual ICollection<IFunctionCondition> Conditions { get; set; } = [];

    public virtual Func<IPluginSettings, CancellationToken, IReadOnlyCollection<ISetting>> SettingsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, IReadOnlyCollection<IChatFunction>> FunctionsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, IReadOnlyCollection<IFunctionAction>> ActionsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, IReadOnlyCollection<IFunctionCondition>> ConditionsCallback { get; set; }

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
