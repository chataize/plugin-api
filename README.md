# Plugin API
Official library for building ChatAIze chatbot add-ons.

## First Steps
1. Install the latest version of the **.NET 9.0** SDK - https://dotnet.microsoft.com/en-us/download/dotnet/9.0.
2. Create a new **C# Class Library** project.
3. Install `ChatAIze.PluginApi` NuGet package.
4. Create a class that implements the `IPluginLoader` interface from the `ChatAIze.Abstractions.Plugins` namespace. Alternatively, you can implement the `IAsyncPluginLoader` interface for asynchronous loading.
5. In the `Load` method (or `LoadAsync`), construct and return a new instance of the `ChatbotPlugin` class with the specified `Id` and `Title`.
6. Add settings, functions, conditions, and actions to your plugin: `plugin.AddStringSetting()` , `plugin.AddFunction()`, `plugin.AddCondition()`, `plugin.AddAction()`, etc.
7. Build the project in **Release** configuration to generate a DLL file.
8. Upload the generated DLL file from the chatbot dashboard: **Integrations > Manage Plugins > Upload**.
9. The chatbot will automatically load the plugin, and its functionality should be available immediately.
10. Verify that the plugin is listed under: **Integrations > Manage Plugins**.

## Installation
### .NET CLI
```bash
dotnet add package ChatAIze.PluginAPI
```
### Package Manager Console
```powershell
Install-Package ChatAIze.PluginAPI
```
## Plugin Base
The `Load` and `LoadAsync` methods are called automatically when the chatbot server starts or when the plugin is uploaded via the dashboard.
### Synchronous Loader
```csharp
using ChatAIze.Abstractions.Plugins;
using ChatAIze.PluginApi;

namespace ExamplePlugin;

public class ExamplePluginLoader : IPluginLoader
{
    public IChatbotPlugin Load()
    {
        var plugin = new ChatbotPlugin
        {
            Id = "com.example.plugin",
            Title = "Example Plugin"
        };

        return plugin;
    }
}
```
### Asynchronous Loader
```csharp
using ChatAIze.Abstractions.Plugins;
using ChatAIze.PluginApi;

namespace ExamplePlugin;

public class ExamplePluginLoader : IAsyncPluginLoader
{
    public async ValueTask<IChatbotPlugin> LoadAsync(CancellationToken cancellationToken = default)
    {
        await Task.Delay(1000, cancellationToken); // Simulate some async work

        var plugin = new ChatbotPlugin
        {
            Id = "com.example.plugin",
            Title = "Example Plugin"
        };

        return plugin;
    }
}
```

## Functions
All functions added to the plugin are discoverable by the chatbot and can be called on-demand during conversations.
Start by writing a normal C# method, then register it with the plugin using `plugin.AddFunction()`.
- Function names must be unique across all loaded plugins.
- Asynchronous functions and cancellation tokens are supported.
- It is recommended to use simple parameter and return types (`string`, `int`, `bool`, etc.) or custom classes with primitive properties.
- Returned objects are serialized to JSON and visible to the chatbot.
- You can optionally add `IFunctionContext` parameter to access conversation details, user info, plugin settings, and more.
```csharp
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.PluginApi;

namespace ExamplePlugin;

public class ExamplePluginLoader : IPluginLoader
{
    private readonly HttpClient _httpClient = new();

    public IChatbotPlugin Load()
    {
        var plugin = new ChatbotPlugin
        {
            Id = "exampleplugin",
            Title = "Example Plugin",
        };

        plugin.AddFunction(GetDogPhoto);
        return plugin;
    }

    public async Task<string> GetDogPhoto(IFunctionContext context, CancellationToken cancellationToken = default)
    {
        if (context.User.Email is null || !context.User.Email.EndsWith("@example.com", StringComparison.OrdinalIgnoreCase))
        {
            return "You cannot use this function.";
        }

        if (context.IsPreview || context.IsDebugModeOn || context.IsCommunicationSandboxed)
        {
            return "Testing...";
        }

        var response = await _httpClient.GetStringAsync("https://dog.ceo/api/breeds/image/random", cancellationToken);
        return response;
    }
}
```
