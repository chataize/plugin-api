using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsParagraph : ISettingsParagraph
{
    public SettingsParagraph() { }

    [SetsRequiredMembers]
    public SettingsParagraph(string id, string? content = null, bool isDisabled = false)
    {
        Id = id;
        Content = content;
        IsDisabled = isDisabled;
    }

    public virtual required string Id { get; set; }

    public virtual string? Content { get; set; }

    public virtual bool IsDisabled { get; set; }
}

public static class SettingsParagraphExtensions
{
    public static void AddSettingsParagraph(this IEditableSettingsContainer container, string id, string? content = null, bool isDisabled = false)
    {
        var paragraph = new SettingsParagraph(id, content, isDisabled);
        container.Settings.Add(paragraph);
    }

    public static void AddSettingsParagraph(this ICollection<ISetting> collection, string id, string? content = null, bool isDisabled = false)
    {
        var paragraph = new SettingsParagraph(id, content, isDisabled);
        collection.Add(paragraph);
    }
}
