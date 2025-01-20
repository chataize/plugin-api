using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi;

public class FunctionCondition : IFunctionCondition, ISettingsContainer, IEditableSettingsContainer
{
    public FunctionCondition()
    {
        SettingsCallback ??= _ => (IReadOnlyCollection<ISetting>)Settings;
    }

    [SetsRequiredMembers]
    public FunctionCondition(string id, string title, Delegate callback, string? description = null, string? iconUrl = null, params ICollection<ISetting>? settings) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        IconUrl = iconUrl;
        Callback = callback;
        Settings = settings ?? [];
    }

    public virtual required string Id { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? IconUrl { get; set; }

    public virtual required Delegate Callback { get; set; }

    public virtual ICollection<ISetting> Settings { get; set; } = [];

    IReadOnlyCollection<ISetting> ISettingsContainer.Settings => (IReadOnlyCollection<ISetting>)Settings;

    public virtual Func<IReadOnlyDictionary<string, JsonElement>, IReadOnlyCollection<ISetting>> SettingsCallback { get; set; }

    public void AddSetting(ISetting setting)
    {
        Settings.Add(setting);
    }
}
