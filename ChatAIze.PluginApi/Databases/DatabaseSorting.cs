using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Databases;
using ChatAIze.Abstractions.Databases.Enums;

namespace ChatAIze.PluginApi.Databases;

/// <summary>
/// Concrete <see cref="IDatabaseSorting"/> used to describe a sort order in a database query.
/// </summary>
/// <remarks>
/// Use this type with database APIs that accept <see cref="IDatabaseSorting"/> (for example via <see cref="ChatAIze.Abstractions.Databases.IDatabaseManager"/>).
/// The meaning of <see cref="Property"/> and supported fields depend on the underlying database/provider.
/// </remarks>
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
