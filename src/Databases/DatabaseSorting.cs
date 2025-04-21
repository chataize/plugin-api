using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Databases;
using ChatAIze.Abstractions.Databases.Enums;

namespace ChatAIze.PluginApi.Databases;

/// <summary>
/// Represents a sorting rule used to order database query results by a specified property and sort direction.
/// </summary>
public record DatabaseSorting : IDatabaseSorting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseSorting"/> class.
    /// </summary>
    public DatabaseSorting() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseSorting"/> class with the specified property and sort order.
    /// </summary>
    /// <param name="property">The property name to sort by.</param>
    /// <param name="order">The sort direction (ascending or descending).</param>
    [SetsRequiredMembers]
    public DatabaseSorting(string property, SortOrder order)
    {
        Property = property;
        Order = order;
    }

    /// <inheritdoc />
    public virtual required string Property { get; set; }

    /// <inheritdoc />
    public virtual SortOrder Order { get; set; }
}
