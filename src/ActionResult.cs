using System.Diagnostics.CodeAnalysis;

namespace ChatAIze.PluginApi;

public class ActionResult
{
    public ActionResult() { }

    [SetsRequiredMembers]
    public ActionResult(string id, object result, bool isSuccess)
    {
        Id = id;
        Result = result;
        IsSuccess = isSuccess;
    }

    public virtual required string Id { get; set; }

    public virtual required object Result { get; set; }

    public virtual bool IsSuccess { get; set; }
}
