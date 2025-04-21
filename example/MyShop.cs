using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;
using ChatAIze.PluginApi.Settings;

namespace ChatAIze.PluginApi.ExamplePlugin;

/// <summary>
/// A plugin loader that registers the <b>MyShop</b> plugin when the assembly is discovered.
/// </summary>
/// <remarks>
/// The chatbot host will locate this class (because it implements <see cref="IPluginLoader"/>)
/// and invoke <see cref="Load"/> to obtain the concrete <see cref="IChatbotPlugin"/> instance.
/// </remarks>
public class MyShop : IPluginLoader
{
    /// <summary>
    /// Builds and returns a configured instance of the <b>MyShop</b> plugin.
    /// </summary>
    /// <returns>The fully‑configured <see cref="IChatbotPlugin"/>.</returns>
    public IChatbotPlugin Load()
    {
        // ─────────────────────────────────────────────────────────────────────────────
        // Settings – plain scalar controls
        // ─────────────────────────────────────────────────────────────────────────────
        var setting1 = new StringSetting
        {
            Id = "myshop:name",
            Title = "Name",
            Description = "The name of the shop.",
            Placeholder = "Shop",
            DefaultValue = "My Shop",
            MaxLength = 50
        };

        var setting2 = new IntegerSetting
        {
            Id = "myshop:products_per_page",
            Title = "Products per page",
            Description = "The number of products to show per page.",
            Style = IntegerSettingStyle.Stepper,
            DefaultValue = 10,
            MinValue = 1,
            MaxValue = 100
        };

        var setting3 = new IntegerSetting
        {
            Id = "myshop:products_per_row",
            Title = "Products per row",
            Description = "The number of products to show per row.",
            Style = IntegerSettingStyle.Slider,
            DefaultValue = 3,
            MinValue = 1,
            MaxValue = 10
        };

        var setting4 = new BooleanSetting
        {
            Id = "myshop:show_out_of_stock",
            Title = "Show out of stock products",
            Description = "Whether to show products that are out of stock.",
            Style = BooleanSettingStyle.ToggleSwitch,
            DefaultValue = false
        };

        var setting5 = new BooleanSetting
        {
            Id = "myshop:show_prices",
            Title = "Show prices",
            Description = "Whether to show prices for products.",
            Style = BooleanSettingStyle.CheckBox,
            DefaultValue = true
        };

        // ─────────────────────────────────────────────────────────────────────────────
        // Settings – selection controls demonstrated with three different styles
        // ─────────────────────────────────────────────────────────────────────────────
        var setting6 = new SelectionSetting
        {
            Id = "myshop:currency",
            Title = "Currency",
            Description = "The currency to display prices in.",
            Style = SelectionSettingStyle.SegmentedControl,
            DefaultValue = "USD",
            Choices =
            [
                new SelectionChoice { Title = "US Dollar",     Value = "USD" },
                new SelectionChoice { Title = "Euro",          Value = "EUR" },
                new SelectionChoice { Title = "British Pound", Value = "GBP" }
            ]
        };

        var setting7 = new SelectionSetting
        {
            Id = "myshop:currency",
            Title = "Currency",
            Description = "The currency to display prices in.",
            Style = SelectionSettingStyle.RadioButtons,
            DefaultValue = "USD",
            Choices =
            [
                new SelectionChoice { Title = "US Dollar",     Value = "USD" },
                new SelectionChoice { Title = "Euro",          Value = "EUR" },
                new SelectionChoice { Title = "British Pound", Value = "GBP" }
            ]
        };

        var setting8 = new SelectionSetting
        {
            Id = "myshop:currency",
            Title = "Currency",
            Description = "The currency to display prices in.",
            Style = SelectionSettingStyle.DropDown,
            DefaultValue = "USD",
            Choices =
            [
                new SelectionChoice { Title = "US Dollar",     Value = "USD" },
                new SelectionChoice { Title = "Euro",          Value = "EUR" },
                new SelectionChoice { Title = "British Pound", Value = "GBP" }
            ]
        };

        // ─────────────────────────────────────────────────────────────────────────────
        // Settings – date/time variations
        // ─────────────────────────────────────────────────────────────────────────────
        var setting9 = new DateTimeSetting
        {
            Id = "myshop:opening_hours",
            Title = "Opening hours",
            Description = "The opening hours of the shop.",
            Style = DateTimeSettingStyle.DateTime,
            DefaultValue = DateTimeOffset.UtcNow,
            MinValue = DateTimeOffset.UtcNow.AddDays(-7),
            MaxValue = DateTimeOffset.UtcNow.AddDays(7)
        };

        var setting10 = new DateTimeSetting
        {
            Id = "myshop:opening_hours",
            Title = "Opening hours",
            Description = "The opening hours of the shop.",
            Style = DateTimeSettingStyle.DateOnly,
            DefaultValue = DateTimeOffset.UtcNow,
            MinValue = DateTimeOffset.UtcNow.AddDays(-7),
            MaxValue = DateTimeOffset.UtcNow.AddDays(7)
        };

        var setting11 = new DateTimeSetting
        {
            Id = "myshop:opening_hours",
            Title = "Opening hours",
            Description = "The opening hours of the shop.",
            Style = DateTimeSettingStyle.TimeOnly,
            DefaultValue = DateTimeOffset.UtcNow,
            MinValue = DateTimeOffset.UtcNow.AddDays(-7),
            MaxValue = DateTimeOffset.UtcNow.AddDays(7)
        };

        var setting12 = new ListSetting
        {
            Id = "myshop:categories",
            Title = "Categories",
            Description = "The categories of products to show.",
            MaxItems = 5,
            MaxItemLength = 20,
        };

        // ─────────────────────────────────────────────────────────────────────────────
        // Miscellaneous UI elements
        // ─────────────────────────────────────────────────────────────────────────────
        var setting13 = new SettingsButton
        {
            Id = "myshop:open_store",
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
            Id = "myshop:welcome_message",
            Content = "You can use this plugin to display products, manage orders, and more."
        };

        var setting15 = new MapSetting
        {
            Id = "myshop:prices",
            Title = "Prices",
            Description = "The prices of products.",
            KeyPlaceholder = "Product",
            ValuePlaceholder = "Price",
            MaxItems = 5,
            MaxKeyLength = 10,
            MaxValueLength = 20
        };

        // ─────────────────────────────────────────────────────────────────────────────
        // Grouping and sectioning
        // ─────────────────────────────────────────────────────────────────────────────
        var group1 = new SettingsGroup
        {
            Id = "myshop:general_settings",
            Title = "General Settings",
            Description = "Settings for the shop.",
            Settings = [setting3, setting4, setting5]
        };

        var group2 = new SettingsGroup
        {
            Id = "myshop:appearance_settings",
            Title = "Appearance Settings",
            Description = "Settings for the appearance of the shop.",
            Settings = [setting6, setting7, setting8]
        };

        var section1 = new SettingsSection
        {
            Id = "myshop:shop_settings",
            Title = "Shop Settings",
            Description = "Settings for the shop.",
            Settings = [setting2, group1, group2, setting9]
        };

        var section2 = new SettingsSection
        {
            Id = "myshop:order_settings",
            Title = "Order Settings",
            Description = "Settings for orders.",
            Settings = [setting10, setting11, setting12]
        };

        // ─────────────────────────────────────────────────────────────────────────────
        // Plugin instance
        // ─────────────────────────────────────────────────────────────────────────────
        var plugin = new ChatbotPlugin
        {
            Id = new("55bc120a-b623-4d5f-91e6-ae2b9f3bf6e2"),
            Title = "MyShop",
            Description = "A simple shop plugin",
            Version = new Version(1, 0, 0, 0),
            SettingsCallback = _ => ValueTask.FromResult<IReadOnlyCollection<ISetting>>([section1, section2, setting13, setting14, setting15]),
        };

        // ─────────────────────────────────────────────────────────────────────────────
        // Functions, actions, and conditions
        // ─────────────────────────────────────────────────────────────────────────────
        plugin.AddFunction(GetOrderStatus);

        var action1 = new FunctionAction(
            id: "myshop:create_order",
            title: "Create Order",
            callback: () => "order created, id: 3321");

        _ = action1.AddStringSetting(
            id: "productName",
            title: "Product Name");

        plugin.AddAction(action1);

        var condition1 = new FunctionCondition(
            id: "myshop:is_domain_user",
            title: "Is Domain User",
            callback: CheckDomainUser);

        _ = condition1.AddStringSetting(
            id: "domain",
            title: "Domain",
            description: "The domain to check for.");

        plugin.AddCondition(condition1);

        return plugin;
    }

    /// <summary>
    /// Sample function that returns the status of an order.
    /// </summary>
    private static string GetOrderStatus()
    {
        return "Order status: shipped";
    }

    /// <summary>
    /// Sample condition that allows execution only for users whose email ends with a specified domain.
    /// </summary>
    /// <param name="context">Current chat context.</param>
    /// <param name="domain">Domain that the user's email must match.</param>
    /// <returns><see langword="true"/> if the user's email ends with the specified domain; otherwise, <see langword="false"/>.</returns>
    private static bool CheckDomainUser(IConditionContext context, string domain)
    {
        return context.User.Email?.EndsWith(domain, StringComparison.InvariantCultureIgnoreCase) == true;
    }
}
