using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

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
