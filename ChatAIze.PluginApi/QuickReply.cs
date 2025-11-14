using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Chat;

namespace ChatAIze.PluginApi;

/// <inheritdoc cref="IQuickReply"/>
public record QuickReply : IQuickReply
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuickReply"/> class.
    /// </summary>
    public QuickReply() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="QuickReply"/> class with the specified emoji and content.
    /// </summary>
    /// <param name="emoji">The emoji associated with the quick reply.</param>
    /// <param name="content">The text content that will be sent when the quick reply is selected.</param>
    [SetsRequiredMembers]
    public QuickReply(string emoji, string content)
    {
        Emoji = emoji;
        Content = content;
    }

    /// <inheritdoc />
    public virtual required string Emoji { get; set; }

    /// <inheritdoc />
    public virtual required string Content { get; set; }
}
