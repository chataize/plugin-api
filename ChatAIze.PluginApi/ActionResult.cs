using System.Diagnostics.CodeAnalysis;

namespace ChatAIze.PluginApi;

/// <summary>
/// Represents the result of an action execution, including its identifier, outcome value, and success flag.
/// </summary>
public record ActionResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActionResult"/> class.
    /// </summary>
    public ActionResult() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionResult"/> class with the specified values.
    /// </summary>
    /// <param name="id">The identifier of the action that produced this result.</param>
    /// <param name="result">The output or return value from the action.</param>
    /// <param name="isSuccess">A flag indicating whether the action completed successfully.</param>
    [SetsRequiredMembers]
    public ActionResult(string id, object result, bool isSuccess)
    {
        Id = id;
        Result = result;
        IsSuccess = isSuccess;
    }

    /// <summary>
    /// Gets or sets the unique identifier of the action that generated this result.
    /// </summary>
    public virtual required string Id { get; set; }

    /// <summary>
    /// Gets or sets the result value produced by the action.
    /// </summary>
    public virtual required object Result { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the action completed successfully.
    /// </summary>
    public virtual bool IsSuccess { get; set; }
}
