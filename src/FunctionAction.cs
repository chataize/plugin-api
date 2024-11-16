using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

public class FunctionAction : IFunctionAction, IEditableSettingsContainer
{
    public FunctionAction() { }

    [SetsRequiredMembers]
    [OverloadResolutionPriority(1)]
    public FunctionAction(string id, string title, Delegate callback, params ICollection<ISetting>? settings)
    {
        Id = id;
        Title = title;
        Callback = callback;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public FunctionAction(string id, string title, Delegate callback, params ICollection<string>? placeholders)
    {
        Id = id;
        Title = title;
        Callback = callback;
        Placeholders = placeholders ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual required Delegate Callback { get; set; }

    public virtual ICollection<ISetting> Settings { get; set; } = [];

    public virtual ICollection<string> Placeholders { get; set; } = [];

    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;

    IReadOnlyCollection<string> IFunctionAction.Placeholders => (IReadOnlyCollection<string>)Placeholders;

    public virtual void AddSetting(ISetting setting)
    {
        Settings.Add(setting);
    }

    public virtual void AddPlaceholder(string placeholder)
    {
        Placeholders.Add(placeholder);
    }
}
