using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

public record QuickReply : IQuickReply
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
