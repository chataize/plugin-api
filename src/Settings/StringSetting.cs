using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;

namespace ChatAIze.PluginApi.Settings;

public class StringSetting : IStringSetting
{
    public StringSetting() { }

    [SetsRequiredMembers]
    public StringSetting(string id, string? title = null, string? description = null, string? placeholder = null, string? defaultValue = null, TextFieldType textFieldType = TextFieldType.Default, int maxLength = 100, int editorLines = 1, bool isLowercase = false, bool isDisabled = false)
    {
        Id = id;
        Title = title;
        Description = description;
        Placeholder = placeholder;
        DefaultValue = defaultValue;
        TextFieldType = textFieldType;
        MaxLength = maxLength;
        EditorLines = editorLines;
        IsLowercase = isLowercase;
        IsDisabled = isDisabled;
    }

    public virtual required string Id { get; set; }

    public virtual string? Title { get; set; }

    public virtual string? Description { get; set; }

    public virtual string? Placeholder { get; set; }

    public virtual string? DefaultValue { get; set; }

    public virtual object DefaultValueObject => DefaultValue ?? string.Empty;

    public virtual TextFieldType TextFieldType { get; set; }

    public virtual int MaxLength { get; set; } = 100;

    public virtual int EditorLines { get; set; } = 1;

    public virtual bool IsLowercase { get; set; }

    public virtual bool IsDisabled { get; set; }
}

public static class StringSettingExtensions
{
    public static IEditableSettingsContainer AddStringSetting(this IEditableSettingsContainer container, string id, string? title = null, string? description = null, string? placeholder = null, string? defaultValue = null, TextFieldType textFieldType = TextFieldType.Default, int maxLength = 100, int editorLines = 1, bool isLowercase = false, bool isDisabled = false)
    {
        var setting = new StringSetting(id, title, description, placeholder, defaultValue, textFieldType, maxLength, editorLines, isLowercase, isDisabled);
        container.Settings.Add(setting);

        return container;
    }

    public static ICollection<ISetting> AddStringSetting(this ICollection<ISetting> collection, string id, string? title = null, string? description = null, string? placeholder = null, string? defaultValue = null, TextFieldType textFieldType = TextFieldType.Default, int maxLength = 100, int editorLines = 1, bool isLowercase = false, bool isDisabled = false)
    {
        var setting = new StringSetting(id, title, description, placeholder, defaultValue, textFieldType, maxLength, editorLines, isLowercase, isDisabled);
        collection.Add(setting);

        return collection;
    }
}
