using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

public class FunctionAction : IFunctionAction, ISettingsContainer, IEditableSettingsContainer
{
    public FunctionAction()
    {
        SettingsCallback ??= _ => (IReadOnlyCollection<ISetting>)Settings;
        PlaceholdersCallback ??= _ => (IReadOnlyCollection<string>)Placeholders;
    }

    [SetsRequiredMembers]
    [OverloadResolutionPriority(1)]
    public FunctionAction(string id, string title, Delegate callback, string? description = null, string? iconUrl = null, bool runsSilently = false, params ICollection<ISetting>? settings) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        IconUrl = iconUrl;
        RunsSilently = runsSilently;
        Callback = callback;
        Settings = settings ?? [];
    }

    [SetsRequiredMembers]
    public FunctionAction(string id, string title, Delegate callback, string? description = null, string? iconUrl = null, bool runsSilently = false, params ICollection<string>? placeholders) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        IconUrl = iconUrl;
        RunsSilently = runsSilently;
        Callback = callback;
        Placeholders = placeholders ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? IconUrl { get; set; }

    public virtual bool RunsSilently { get; set; }

    public virtual required Delegate Callback { get; set; }

    public virtual ICollection<ISetting> Settings { get; set; } = [];

    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;

    public virtual ICollection<string> Placeholders { get; set; } = [];

    public virtual Func<IReadOnlyDictionary<string, JsonElement>, IReadOnlyCollection<ISetting>> SettingsCallback { get; set; }

    public virtual Func<IReadOnlyDictionary<string, JsonElement>, IReadOnlyCollection<string>> PlaceholdersCallback { get; set; }

    public virtual void AddSetting(ISetting setting)
    {
        Settings.Add(setting);
    }

    public virtual void AddPlaceholder(string placeholder)
    {
        Placeholders.Add(placeholder);
    }
}
