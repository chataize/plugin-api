using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Actions;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Actions;

namespace ChatAIze.PluginApi;

public class ChatbotPlugin : IChatbotPlugin
{
    public ChatbotPlugin()
    {
        SettingsCallback ??= (_, _) => ValueTask.FromResult(Settings);
        FunctionsCallback ??= (_, _) => ValueTask.FromResult(Functions);
        ActionsCallback ??= (_, _) => ValueTask.FromResult(Actions);
    }

    [SetsRequiredMembers]
    public ChatbotPlugin(string id, string title, string? description = null, string? website = null, string? author = null, Version? version = null, DateTimeOffset? releaseTime = null, DateTimeOffset? lastUpdateTime = null, ICollection<IPluginSetting>? settings = null, ICollection<IChatFunction>? functions = null, ICollection<FunctionAction>? actions = null, Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IPluginSetting>>>? settingsCallback = null, Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IChatFunction>>>? functionsCallback = null, Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IFunctionAction>>>? actionsCallback = null)
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

        if (settingsCallback is null)
        {
            SettingsCallback = (_, _) => ValueTask.FromResult(Settings);
        }
        else
        {
            SettingsCallback = settingsCallback;
        }

        if (functionsCallback is null)
        {
            FunctionsCallback = (_, _) => ValueTask.FromResult(Functions);
        }
        else
        {
            FunctionsCallback = functionsCallback;
        }

        if (actionsCallback is null)
        {
            ActionsCallback = (_, _) => ValueTask.FromResult(Actions);
        }
        else
        {
            ActionsCallback = actionsCallback;
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

    public virtual ICollection<IPluginSetting> Settings { get; set; } = [];

    public virtual ICollection<IChatFunction> Functions { get; set; } = [];

    public virtual ICollection<IFunctionAction> Actions { get; set; } = [];

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IPluginSetting>>>? SettingsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IChatFunction>>>? FunctionsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IFunctionAction>>>? ActionsCallback { get; set; }

    public virtual void AddSetttng(IPluginSetting setting)
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
}
