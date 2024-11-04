using ChatAIze.PluginApi.Interfaces;
using ChatAIze.PluginApi.Settings;

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

        var setting1 = new StringSetting
        {
            Key = "my_shop:setting1",
            Title = "Setting 1",
            Description = "This is a test setting",
            DefaultValue = "default value",
            Placeholder = "Enter a value",
            MaxLength = 50,
            EditorLines = 1,
            IsSecret = false
        };

        var setting2 = new IntegerSetting
        {
            Key = "my_shop:setting2",
            Title = "Setting 2",
            Description = "This is a test setting",
            DefaultValue = 5,
            MinValue = 3,
            MaxValue = 10
        };

        var plugin = new ChatbotPlugin
        {
            Id = new("55bc120a-b623-4d5f-91e6-ae2b9f3bf6e2"),
            Name = "MyShop",
            Description = "A simple shop plugin",
            Version = "1.0.0",
            Settings = [setting1, setting2],
            Functions = [getProducts]
        };

        return ValueTask.FromResult<IChatbotPlugin>(plugin);
    }

    private static string GetOrderStatus()
    {
        return "Order status: shipped";
    }
}
