using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi.ExamplePlugin;

public class MyShop : IPluginLoader
{
    public ValueTask<IChatbotPlugin> LoadAsync(CancellationToken cancellationToken = default)
    {
        var getProducts = new ChatFunction
        {
            Name = "get_order_status",
            Description = "Gets the status of an order with the given order id.",
            Parameters =
            [
                new FunctionParameter("order_id", "The order id to get the status for.", typeof(string))
            ],
            Callback = GetOrderStatus
        };

        var plugin = new ChatbotPlugin
        {
            Id = new("55bc120a-b623-4d5f-91e6-ae2b9f3bf6e2"),
            Name = "MyShop",
            Description = "A simple shop plugin",
            Version = "1.0.0",
            Functions = [getProducts]
        };

        return ValueTask.FromResult<IChatbotPlugin>(plugin);
    }

    private static string GetOrderStatus()
    {
        return "Order status: shipped";
    }
}
