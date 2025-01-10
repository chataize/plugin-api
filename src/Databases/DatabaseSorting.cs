using System.Diagnostics.CodeAnalysis;
using ChatAIze.Abstractions.Databases;
using ChatAIze.Abstractions.Databases.Enums;

namespace ChatAIze.PluginApi.Databases;

public record DatabaseSorting : IDatabaseSorting
{
    public DatabaseSorting() { }

    [SetsRequiredMembers]
    public DatabaseSorting(string property, SortOrder order)
    {
        Property = property;
        Order = order;
    }

    public virtual required string Property { get; set; }

    public virtual SortOrder Order { get; set; }
}
