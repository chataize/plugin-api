using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi;

public class ChatbotPlugin : IChatbotPlugin
{
    public ChatbotPlugin()
    {
        SettingsCallback ??= (_, _) => ValueTask.FromResult(Settings);
        FunctionsCallback ??= (_, _) => ValueTask.FromResult(Functions);
    }

    [SetsRequiredMembers]
    public ChatbotPlugin(Guid id, string title, string? description = null, string? website = null, string? author = null, string version = "1.0.0", DateTimeOffset? releaseTime = null, DateTimeOffset? lastUpdateTime = null, ICollection<IPluginSetting>? settings = null, ICollection<IChatFunction>? functions = null, Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IPluginSetting>>>? settingsCallback = null, Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IChatFunction>>>? functionsCallback = null, IDictionary<string, Func<IDictionary<string, object?>?, CancellationToken, ValueTask<object?>>>? sharedMethods = null)
    {
        Id = id;
        Title = title;
        Description = description;
        Website = website;
        Author = author;
        Version = version;
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

        SharedMethods = sharedMethods ?? new Dictionary<string, Func<IDictionary<string, object?>?, CancellationToken, ValueTask<object?>>>();
    }

    public virtual required Guid Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? Website { get; set; }

    public virtual string? Author { get; set; }

    public virtual string Version { get; set; } = "1.0.0";

    public virtual DateTimeOffset? ReleaseTime { get; set; }

    public virtual DateTimeOffset? LastUpdateTime { get; set; }

    public IDictionary<string, Func<IDictionary<string, object?>?, CancellationToken, ValueTask<object?>>> SharedMethods { get; set; } = new Dictionary<string, Func<IDictionary<string, object?>?, CancellationToken, ValueTask<object?>>>();

    public virtual ICollection<IPluginSetting> Settings { get; set; } = [];

    public virtual ICollection<IChatFunction> Functions { get; set; } = [];

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IPluginSetting>>>? SettingsCallback { get; set; }

    public virtual Func<IPluginSettings, CancellationToken, ValueTask<ICollection<IChatFunction>>>? FunctionsCallback { get; set; }

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

    public virtual void AddSharedMethod(string name, Func<IDictionary<string, object?>?, CancellationToken, ValueTask<object?>> method)
    {
        SharedMethods[name] = method;
    }
}
