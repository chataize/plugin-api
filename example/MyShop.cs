﻿using ChatAIze.Abstractions.Chat;
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
                new FunctionParameter(typeof(string), "order_id", "The order id to get the status for.")
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
            new SelectionChoice { Title = "US Dollar", Value = "USD" },
            new SelectionChoice { Title = "Euro", Value = "EUR" },
            new SelectionChoice { Title = "British Pound", Value = "GBP" }
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
            new SelectionChoice { Title = "US Dollar", Value = "USD" },
            new SelectionChoice { Title = "Euro", Value = "EUR" },
            new SelectionChoice { Title = "British Pound", Value = "GBP" }
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
                new SelectionChoice { Title = "US Dollar", Value = "USD" },
                new SelectionChoice { Title = "Euro", Value = "EUR" },
                new SelectionChoice { Title = "British Pound", Value = "GBP" }
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
            Key = "myshop:open_store",
            Title = "Open Store",
            Description = "Opens the store in a new tab.",
            Callback = (_) =>
            {
                Console.WriteLine("Opening store...");
                return ValueTask.CompletedTask;
            }
        };

        var setting14 = new SettingsParagraph
        {
            Key = "myshop:welcome_message",
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
            Key = "myshop:general_settings",
            Title = "General Settings",
            Description = "Settings for the shop.",
            Settings = [setting3, setting4, setting5]
        };

        var group2 = new SettingsGroup
        {
            Key = "myshop:appearance_settings",
            Title = "Appearance Settings",
            Description = "Settings for the appearance of the shop.",
            Settings = [setting6, setting7, setting8]
        };

        var section1 = new SettingsSection
        {
            Key = "myshop:shop_settings",
            Title = "Shop Settings",
            Description = "Settings for the shop.",
            Settings = [setting2, group1, group2, setting9]
        };

        var section2 = new SettingsSection
        {
            Key = "myshop:order_settings",
            Title = "Order Settings",
            Description = "Settings for orders.",
            Settings = [setting10, setting11, setting12]
        };

        var plugin = new ChatbotPlugin
        {
            Id = new("55bc120a-b623-4d5f-91e6-ae2b9f3bf6e2"),
            Title = "MyShop",
            Description = "A simple shop plugin",
            Version = "1.0.0",
            SettingsCallback = (_) => ValueTask.FromResult<ICollection<IPluginSetting>>([section1, section2, setting13, setting14, setting15]),
            FunctionsCallback = (_) => ValueTask.FromResult<ICollection<IChatFunction>>([getProducts])
        };

        return ValueTask.FromResult<IChatbotPlugin>(plugin);
    }

    private static string GetOrderStatus()
    {
        return "Order status: shipped";
    }
}
