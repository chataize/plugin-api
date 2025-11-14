using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Databases;
using ChatAIze.Abstractions.Databases.Enums;

namespace ChatAIze.PluginApi.Databases;

/// <summary>
/// Represents a filter condition used in a database query, including the property, type of comparison, value, and optional modifiers.
/// </summary>
public record DatabaseFilter : IDatabaseFilter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseFilter"/> class.
    /// </summary>
    public DatabaseFilter() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseFilter"/> class with specified filter criteria.
    /// </summary>
    /// <param name="property">The property name to filter on.</param>
    /// <param name="type">The type of filter to apply (e.g. equals, contains).</param>
    /// <param name="value">The value to compare the property against, if applicable.</param>
    /// <param name="options">Optional flags that control filter behavior (e.g. case insensitivity).</param>
    [SetsRequiredMembers]
    public DatabaseFilter(string property, FilterType type, string? value, FilterOptions options = FilterOptions.None)
    {
        Property = property;
        Type = type;
        Value = value;
        Options = options;
    }

    /// <inheritdoc />
    public virtual required string Property { get; set; }

    /// <inheritdoc />
    public virtual FilterType Type { get; set; }

    /// <inheritdoc />
    public virtual string? Value { get; set; }

    /// <inheritdoc />
    public virtual FilterOptions Options { get; set; }
}
