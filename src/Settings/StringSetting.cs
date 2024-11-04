using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class StringSetting : IStringSetting
{
    public StringSetting() { }

    [SetsRequiredMembers]
    public StringSetting(string key, string title, string? description = null, string? placeholder = null, string? defaultValue = null, int maxLength = 100, int editorLines = 1, bool isSecret = false)
    {
        Key = key;
        Title = title;
        Description = description;
        Placeholder = placeholder;
        DefaultValue = defaultValue;
        MaxLength = maxLength;
        EditorLines = editorLines;
        IsSecret = isSecret;
    }

    public virtual required string Key { get; set; }

    public virtual required string Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? Placeholder { get; set; }

    public virtual string? DefaultValue { get; set; }

    public virtual int MaxLength { get; set; } = 100;

    public virtual int EditorLines { get; set; } = 1;

    public virtual bool IsSecret { get; set; }
}
