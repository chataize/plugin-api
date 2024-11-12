using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Actions.Properties;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Actions.Properties;

public class StringProperty : IStringProperty
{
    public StringProperty() { }

    [SetsRequiredMembers]
    public StringProperty(string parameter, string? title = null, string? description = null, string? placeholder = null, string? defaultValue = null, TextFieldType textFieldType = TextFieldType.Default, int maxLength = 100, int editorLines = 1, bool isSecure = false, bool isDisabled = false)
    {
        Parameter = parameter;
        Title = title;
        Description = description;
        Placeholder = placeholder;
        DefaultValue = defaultValue;
        TextFieldType = textFieldType;
        MaxLength = maxLength;
        EditorLines = editorLines;
        IsDisabled = isDisabled;
    }

    public virtual required string Parameter { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? Placeholder { get; set; }

    public virtual string? DefaultValue { get; set; }

    public virtual TextFieldType TextFieldType { get; set; }

    public virtual int MaxLength { get; set; } = 100;

    public virtual int EditorLines { get; set; } = 1;

    public virtual bool IsDisabled { get; set; }
}

public static class StringPropertyExtensions
{
    public static void AddStringProperty(this FunctionAction action, string parameter, string? title = null, string? description = null, string? placeholder = null, string? defaultValue = null, TextFieldType textFieldType = TextFieldType.Default, int maxLength = 100, int editorLines = 1, bool isSecure = false, bool isDisabled = false)
    {
        var property = new StringProperty(parameter, title, description, placeholder, defaultValue, textFieldType, maxLength, editorLines, isSecure, isDisabled);
        action.Properties.Add(property);
    }
}
