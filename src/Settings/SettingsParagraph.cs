using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi.Actions;

namespace ChatAIze.PluginApi.Settings;

public class SettingsParagraph : ISettingsParagraph
{
    public SettingsParagraph() { }

    [SetsRequiredMembers]
    public SettingsParagraph(string key, string? content = null, bool isDisabled = false)
    {
        Key = key;
        Content = content;
        IsDisabled = isDisabled;
    }

    public virtual required string Key { get; set; }

    public virtual string? Content { get; set; }

    public virtual bool IsDisabled { get; set; }
}

public static class SettingsParagraphExtensions
{
    public static void AddSettingsParagraph(this ChatbotPlugin plugin, string key, string? content = null, bool isDisabled = false)
    {
        var paragraph = new SettingsParagraph(key, content, isDisabled);
        plugin.Settings.Add(paragraph);
    }

    public static void AddSettingsParagraph(this FunctionAction action, string key, string? content = null, bool isDisabled = false)
    {
        var paragraph = new SettingsParagraph(key, content, isDisabled);
        action.Settings.Add(paragraph);
    }
}
