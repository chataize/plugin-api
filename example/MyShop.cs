using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;
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
            Key = "myshop:name",
            Title = "Name",
            Description = "The name of the shop.",
            Placeholder = "Shop",
            DefaultValue = "My Shop",
            MaxLength = 50
        };

        var setting2 = new IntegerSetting
        {
            Key = "myshop:products_per_page",
            Title = "Products per page",
            Description = "The number of products to show per page.",
            Style = IntegerSettingStyle.Stepper,
            DefaultValue = 10,
            MinValue = 1,
            MaxValue = 100
        };

        var setting3 = new IntegerSetting
        {
            Key = "myshop:products_per_row",
            Title = "Products per row",
            Description = "The number of products to show per row.",
            Style = IntegerSettingStyle.Slider,
            DefaultValue = 3,
            MinValue = 1,
            MaxValue = 10
        };

        var setting4 = new BooleanSetting
        {
            Key = "myshop:show_out_of_stock",
            Title = "Show out of stock products",
            Description = "Whether to show products that are out of stock.",
            Style = BooleanSettingStyle.ToggleSwitch,
            DefaultValue = false
        };

        var setting5 = new BooleanSetting
        {
            Key = "myshop:show_prices",
            Title = "Show prices",
            Description = "Whether to show prices for products.",
            Style = BooleanSettingStyle.CheckBox,
            DefaultValue = true
        };

        var setting6 = new SelectionSetting
        {
            Key = "myshop:currency",
            Title = "Currency",
            Description = "The currency to display prices in.",
            Style = SelectionSettingStyle.SegmentedControl,
            DefaultValue = "USD",
            Choices =
            [
                new SelectionChoice("USD", "US Dollar"),
                new SelectionChoice("EUR", "Euro"),
                new SelectionChoice("GBP", "British Pound"),
            ]
        };

        var setting7 = new SelectionSetting
        {
            Key = "myshop:currency",
            Title = "Currency",
            Description = "The currency to display prices in.",
            Style = SelectionSettingStyle.RadioButtons,
            DefaultValue = "USD",
            Choices =
           [
               new SelectionChoice("USD", "US Dollar"),
                new SelectionChoice("EUR", "Euro"),
                new SelectionChoice("GBP", "British Pound"),
            ]
        };

        var setting8 = new SelectionSetting
        {
            Key = "myshop:currency",
            Title = "Currency",
            Description = "The currency to display prices in.",
            Style = SelectionSettingStyle.DropDown,
            DefaultValue = "USD",
            Choices =
            [
               new SelectionChoice("USD", "US Dollar"),
                new SelectionChoice("EUR", "Euro"),
                new SelectionChoice("GBP", "British Pound"),
            ]
        };

        var setting9 = new DateTimeSetting
        {
            Key = "myshop:opening_hours",
            Title = "Opening hours",
            Description = "The opening hours of the shop.",
            Style = DateTimeSettingStyle.DateTime,
            DefaultValue = DateTimeOffset.UtcNow,
            MinValue = DateTimeOffset.UtcNow.AddDays(-7),
            MaxValue = DateTimeOffset.UtcNow.AddDays(7)
        };

        var setting10 = new DateTimeSetting
        {
            Key = "myshop:opening_hours",
            Title = "Opening hours",
            Description = "The opening hours of the shop.",
            Style = DateTimeSettingStyle.DateTime,
            DefaultValue = DateTimeOffset.UtcNow,
            MinValue = DateTimeOffset.UtcNow.AddDays(-7),
            MaxValue = DateTimeOffset.UtcNow.AddDays(7)
        };

        var setting11 = new DateTimeSetting
        {
            Key = "myshop:opening_hours",
            Title = "Opening hours",
            Description = "The opening hours of the shop.",
            Style = DateTimeSettingStyle.TimeOnly,
            DefaultValue = DateTimeOffset.UtcNow,
            MinValue = DateTimeOffset.UtcNow.AddDays(-7),
            MaxValue = DateTimeOffset.UtcNow.AddDays(7)
        };

        var setting12 = new ListSetting
        {
            Key = "myshop:categories",
            Title = "Categories",
            Description = "The categories of products to show.",
            MaxItems = 5,
            MaxItemLength = 20,
        };

        var setting13 = new SettingsButton
        {
            Title = "Open Store",
            Description = "Opens the store in a new tab.",
            Callback = () =>
            {
                Console.WriteLine("Opening store...");
                return ValueTask.CompletedTask;
            }
        };

        var setting14 = new SettingsParagraph
        {
            Title = "Welcome to My Shop!",
            Content = "You can use this plugin to display products, manage orders, and more."
        };

        var setting15 = new MapSetting
        {
            Key = "myshop:prices",
            Title = "Prices",
            Description = "The prices of products.",
            KeyPlaceholder = "Product",
            ValuePlaceholder = "Price",
            MaxItems = 5,
            MaxKeyLength = 10,
            MaxValueLength = 20
        };

        var group1 = new SettingsGroup
        {
            Title = "General Settings",
            Description = "Settings for the shop.",
            Settings = [setting3, setting4, setting5]
        };

        var group2 = new SettingsGroup
        {
            Title = "Appearance Settings",
            Description = "Settings for the appearance of the shop.",
            Settings = [setting6, setting7, setting8]
        };

        var section1 = new SettingsSection
        {
            Title = "Shop Settings",
            Description = "Settings for the shop.",
            Settings = [setting2, group1, group2, setting9]
        };

        var section2 = new SettingsSection
        {
            Title = "Order Settings",
            Description = "Settings for orders.",
            Settings = [setting10, setting11, setting12]
        };

        var plugin = new ChatbotPlugin
        {
            Id = new("55bc120a-b623-4d5f-91e6-ae2b9f3bf6e2"),
            Name = "MyShop",
            Description = "A simple shop plugin",
            Version = "1.0.0",
            Settings = [section1, section2, setting13, setting14, setting15],
            Functions = [getProducts]
        };

        return ValueTask.FromResult<IChatbotPlugin>(plugin);
    }

    private static string GetOrderStatus()
    {
        return "Order status: shipped";
    }
}
