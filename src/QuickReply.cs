using System.Diagnostics.CodeAnalysis;

namespace ChatAIze.PluginApi;

public record QuickReply
{
    public QuickReply() { }

    [SetsRequiredMembers]
    public QuickReply(string emoji, string content)
    {
        Emoji = emoji;
        Content = content;
    }

    public virtual required string Emoji { get; set; }

    public virtual required string Content { get; set; }
}
