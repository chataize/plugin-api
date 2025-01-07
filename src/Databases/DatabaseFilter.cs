using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Databases;

namespace ChatAIze.PluginApi.Databases;

public class DatabaseFilter
{
    public DatabaseFilter() { }

    [SetsRequiredMembers]
    public DatabaseFilter(string property, FilterType type, string? value, FilterOptions options = FilterOptions.None)
    {
        Property = property;
        Type = type;
        Value = value;
        Options = options;
    }

    public virtual required string Property { get; set; }

    public virtual FilterType Type { get; set; }

    public virtual string? Value { get; set; }

    public virtual FilterOptions Options { get; set; }
}
