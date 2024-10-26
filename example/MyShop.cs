using ChatAIze.PluginApi.Enums;
using ChatAIze.PluginApi.Interfaces;

namespace ChatAIze.PluginApi.ExamplePlugin;

public class MyShop : IPluginLoader
{
    public ValueTask<IChatbotPlugin> LoadAsync()
    {
        var getProducts = new ChatbotFunction
        {
            Name = "get_order_status",
            Description = "Gets the status of an order with the given order id.",
            Parameters =
            [
                new FunctionParameter
                {
                    Name = "id",
                    Type = ParameterType.Text,
                    Description = "The category of the products",
                }
            ],
            Callback = () => ValueTask.FromResult<object?>("Order status: shipped")
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
}
