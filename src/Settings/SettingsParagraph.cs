using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class SettingsParagraph : ISettingsParagraph
{
    public SettingsParagraph() { }

    [SetsRequiredMembers]
    public SettingsParagraph(string title, string content)
    {
        Title = title;
        Content = content;
    }

    [SetsRequiredMembers]
    public SettingsParagraph(string title, string? description, string content)
    {
        Title = title;
        Description = description;
        Content = content;
    }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual required string Content { get; set; }
}
