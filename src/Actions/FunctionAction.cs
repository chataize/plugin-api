using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Actions;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Actions;

public class FunctionAction : IFunctionAction
{
    public FunctionAction() { }

    [SetsRequiredMembers]
    public FunctionAction(string key, string title, Func<IDictionary<string, object>, ValueTask<object>>? callback = null, ICollection<IPluginSetting>? settings = null)
    {
        Key = key;
        Title = title;
        Callback = callback;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public FunctionAction(string key, string title, Func<IDictionary<string, object>, ValueTask<object>>? callback = null, params IPluginSetting[] settings)
    {
        Key = key;
        Title = title;
        Callback = callback;
        Settings = settings ?? [];
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual Func<IDictionary<string, object>, ValueTask<object>>? Callback { get; set; }

    public virtual ICollection<IPluginSetting> Settings { get; set; } = [];

    public void AddSetting(IPluginSetting setting)
    {
        Settings.Add(setting);
    }
}
