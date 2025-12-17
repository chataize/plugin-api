# ChatAIze.PluginApi

`ChatAIze.PluginApi` is the official SDK for building plugins (add-ons) for `ChatAIze.Chatbot`.

It builds on top of `ChatAIze.Abstractions` and provides convenient implementations and helpers for:

- **Plugins**: `ChatbotPlugin` (a base implementation of `IChatbotPlugin`).
- **LLM tools / function calling**: `ChatFunction` (+ `FunctionParameter` for explicit schemas).
- **Workflow building blocks** (for dashboard “integration functions”): `FunctionAction`, `FunctionCondition`.
- **Settings UI models**: `StringSetting`, `SelectionSetting`, `IntegerSetting`, `BooleanSetting`, sections/groups, etc.

If you want a working example, start with `ChatAIze.PluginApi.ExamplePlugin/MyShop.cs`.

## TL;DR

- Create a `net10.0` class library and reference `ChatAIze.PluginApi`.
- Implement `IPluginLoader` (or `IAsyncPluginLoader`) and return a `ChatbotPlugin` with `Id`, `Title`, and `Version`.
- Add settings/tools/actions/conditions (either via the collections or callback properties).
- Build `Release` and deploy to ChatAIze.Chatbot (upload a single `.dll`, or copy the whole output folder for plugins with dependencies).

## What you can build

- **Plugin settings** (dashboard UI): Admin-configured values like API keys and feature toggles.
- **Tools** (LLM function calling): Model-callable functions that run code and return structured output.
- **Workflow actions**: Reusable steps that admins compose into “integration functions” in the dashboard.
- **Workflow conditions**: Gates that allow/deny an integration function for the current chat/user.

## Contents

- [TL;DR](#tldr)
- [Requirements](#requirements)
- [Install](#install)
- [Quick Start](#quick-start-minimal-plugin)
- [Deploy](#deploy-to-chataizechatbot)
- [Release Checklist](#plugin-release-checklist)
- [Core Concepts](#core-concepts)
  - [Stable IDs](#stable-ids-plugins-settings-tools-actions-conditions)
  - [Lifetime & Concurrency](#lifetime-concurrency-and-disposal)
  - [Context Objects](#context-objects-what-you-can-access-at-runtime)
  - [Glossary](#glossary-chataizechatbot-terms)
- [Tools](#tools-llm-callable-functions)
- [Settings](#settings-plugin-configuration-ui)
  - [List Settings](#list-settings-listsetting)
  - [Map Settings](#map-settings-mapsetting)
- [Workflow Actions](#workflow-actions-integration-function-steps)
- [Workflow Conditions](#workflow-conditions-gates)
- [Useful Patterns](#useful-patterns)
- [Troubleshooting](#troubleshooting)
- [Examples](#examples)

## Requirements

- **Target framework**: `net10.0` (matches the current `ChatAIze.Chatbot` host).
- **.NET SDK**: install the `.NET 10` SDK (or the SDK required by your target ChatAIze.Chatbot version).

## Install

### .NET CLI

```bash
dotnet add package ChatAIze.PluginApi
```

### Package Manager Console

```powershell
Install-Package ChatAIze.PluginApi
```

## Quick Start (Minimal Plugin)

1) Create a class library:

```bash
dotnet new classlib -n MyCompany.MyPlugin -f net10.0
cd MyCompany.MyPlugin
dotnet add package ChatAIze.PluginApi
```

2) Add a plugin loader (the host discovers plugins in this order):

1. a type implementing `IAsyncPluginLoader`,
2. a type implementing `IPluginLoader`,
3. any non-abstract type implementing `IChatbotPlugin` (constructed via `Activator.CreateInstance`).

### Synchronous loader (`IPluginLoader`)

```csharp
using ChatAIze.Abstractions.Plugins;
using ChatAIze.PluginApi;

namespace MyCompany.MyPlugin;

public sealed class MyPluginLoader : IPluginLoader
{
    public IChatbotPlugin Load()
    {
        return new ChatbotPlugin
        {
            Id = "com.mycompany.myplugin",
            Title = "My Plugin",
            Description = "Adds custom tools and workflow actions.",
            Version = new Version(1, 0, 0)
        };
    }
}
```

### Asynchronous loader (`IAsyncPluginLoader`)

```csharp
using ChatAIze.Abstractions.Plugins;
using ChatAIze.PluginApi;

namespace MyCompany.MyPlugin;

public sealed class MyPluginLoader : IAsyncPluginLoader
{
    public async ValueTask<IChatbotPlugin> LoadAsync(CancellationToken cancellationToken = default)
    {
        // Do any async initialization here (warmup, migrations, etc.)
        await Task.Delay(10, cancellationToken);

        return new ChatbotPlugin
        {
            Id = "com.mycompany.myplugin",
            Title = "My Plugin",
            Version = new Version(1, 0, 0)
        };
    }
}
```

Tip: `ChatbotPlugin` defaults to returning its in-memory `Settings` / `Functions` / `Actions` / `Conditions` collections from the callback properties. You can either populate the collections directly or override the callback properties to return context-dependent definitions.

3) Build:

```bash
dotnet build -c Release
```

## Deploy to ChatAIze.Chatbot

ChatAIze.Chatbot loads plugins from `.dll` files in the `plugins/` folder, or via dashboard upload.

### Option A: Upload from the dashboard (single `.dll`)

Dashboard → Integrations → Plugins → Upload.

This is the simplest workflow, but it uploads **only one file**. If your plugin depends on additional assemblies, prefer file-copy deployment.

### Option B: Copy files to `plugins/` (recommended for plugins with dependencies)

Copy at least:

- `MyCompany.MyPlugin.dll`
- `MyCompany.MyPlugin.deps.json`
- any dependency `.dll` files from your output folder

into the chatbot server’s `plugins/` directory.

Tip: If you have extra dependencies, the safest approach is to copy everything from `bin/Release/net10.0/` (except `.pdb` if you don’t want symbols).

### Plugin release checklist

- Target `net10.0` (match the host).
- Set `IChatbotPlugin.Id` and keep it stable (loading a plugin with the same id replaces the previous one).
- Set `IChatbotPlugin.Version` (ChatAIze.Chatbot treats missing versions as invalid).
- Namespace plugin-level settings (`com.mycompany.myplugin:api_key`) to avoid collisions with other plugins.
- Prefer named methods for tools (`AddFunction(MyTool)`), and keep tool names unique.
- Respect `IsPreview` / `IsCommunicationSandboxed` before doing side effects (HTTP calls, emails/SMS, writes to external systems).
- Don’t log secrets (API keys/tokens).
- If you deploy dependencies, prefer copying the whole output folder to `plugins/` (don’t rely on dashboard upload for multi-file plugins).

<details>
<summary>How plugin loading works (advanced)</summary>

ChatAIze.Chatbot loads plugins with an isolated, unloadable `AssemblyLoadContext`:

- The host copies the entire `plugins/` directory to a temporary folder before loading, so the original files are not locked.
- Each plugin assembly is loaded into its own collectible `AssemblyLoadContext` (so it can be unloaded/replaced).
- A set of assemblies is treated as *shared contracts* and always resolved from the host (for example: `ChatAIze.Abstractions*`, `ChatAIze.PluginApi*`, `ChatAIze.Utilities*`, and some `Microsoft.Extensions.*` abstractions).
- Any other dependency must be resolvable via the plugin’s `.deps.json` (and present in the `plugins/` folder).

This is why dashboard upload is best for “single dll” plugins, and file-copy deployment is best for plugins with external dependencies.
</details>

## Core Concepts

### Stable IDs (Plugins, Settings, Tools, Actions, Conditions)

ChatAIze.Chatbot persists various identifiers, so treat these as **stable contracts**:

- `IChatbotPlugin.Id`: used to identify and replace already-loaded plugins.
- `ISetting.Id`: persisted as a key in the host’s plugin settings store.
- `IChatFunction.Name`: becomes a tool name for the model (and must be unique across all installed plugins + integration functions).
- `IFunctionAction.Id` / `IFunctionCondition.Id`: stored in integration function definitions.

Tool name matching in the ChatAIze stack uses a tolerant “normalized” comparison (case/spacing/punctuation-insensitive), so treat names like `get-order`, `get order`, and `GetOrder` as equivalent when thinking about collisions.

Recommendation:

- Use a reverse-DNS style prefix: `com.mycompany.myplugin:...`, or a GUID string.
- For **plugin-level settings**: always namespace (shared global keyspace across plugins).
- For **action/condition setting ids**: keep them simple (they are local to the action/condition settings dictionary and are used for delegate binding).

### Lifetime, concurrency, and disposal

- A plugin instance is typically a **singleton** for the lifetime of the host process.
- Your callbacks (tools/actions/conditions/settings callbacks) can be invoked **concurrently** for multiple chats/users.
- Avoid storing per-chat/per-user state on the plugin instance; use the context objects instead.
- If your plugin needs cleanup, implement `IDisposable` and/or `IAsyncDisposable`. The host will attempt to dispose plugins on unload.

### Context objects (what you can access at runtime)

Every callback can optionally accept a context parameter:

- `IChatbotContext` (used in the dashboard): `Settings`, `Databases`, `Log(...)`
- `IChatContext` (per conversation): adds `ChatId`, `User`, `IsPreview`, `IsDebugModeOn`, `IsCommunicationSandboxed`, `GetPlugin<T>(...)`
- `IFunctionContext` (tools): adds knowledge search, quick replies, forms, prompt override, status/progress
- `IActionContext` (workflow actions): adds action indices/results + placeholder APIs
- `IConditionContext` (workflow conditions): currently just `IChatContext`

### Glossary (ChatAIze.Chatbot terms)

- **Plugin**: a loaded `.dll` that returns settings, tools, actions and/or conditions.
- **Tool / function**: an `IChatFunction` exposed to the model as a callable tool (LLM function calling).
- **Integration function**: a dashboard-configured function (also an `IChatFunction`) that executes a workflow of actions/conditions.
- **Workflow action**: an `IFunctionAction` step used inside an integration function (not exposed to the model directly).
- **Workflow condition**: an `IFunctionCondition` gate evaluated before an integration function runs.
- **Placeholder**: a named value (`{placeholder}`) produced by actions and injected into later settings as plain-text substitution (supports nested access like `{ticket.id}` when the placeholder is a JSON object).

## Tools (LLM-Callable Functions)

Tools are `IChatFunction` instances returned by `IChatbotPlugin.FunctionsCallback`. They are presented to the language model as callable tools.

### Register a tool from a normal C# method

```csharp
using System.ComponentModel;
using ChatAIze.Abstractions.Chat;
using ChatAIze.Abstractions.Plugins;
using ChatAIze.PluginApi;
using Microsoft.Extensions.Logging;

public sealed class MyPluginLoader : IPluginLoader
{
    public IChatbotPlugin Load()
    {
        var plugin = new ChatbotPlugin
        {
            Id = "com.mycompany.myplugin",
            Title = "My Plugin",
            Version = new Version(1, 0, 0),
        };

        plugin.AddFunction(GetOrderStatusAsync);
        return plugin;
    }

    [Description("Gets the status of an order by id.")]
    private static async Task<string> GetOrderStatusAsync(
        IFunctionContext context,
        [Description("Order id shown to the customer.")] string orderId,
        CancellationToken cancellationToken = default)
    {
        var apiKey = await context.Settings.GetAsync("com.mycompany.myplugin:api_key", "", cancellationToken);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return "Error: Plugin is not configured (missing api_key).";
        }

        // Always respect preview/sandbox flags for side effects.
        if (context.IsPreview || context.IsCommunicationSandboxed)
        {
            context.Log(LogLevel.Information, $"(preview) Returning fake status for {orderId}.");
            return $"Order {orderId} is shipped. (preview)";
        }

        // TODO: call your external system here.
        return $"Order {orderId} is shipped.";
    }
}
```

### Tool parameter binding and naming

- Tool argument JSON is produced by the model using **snake_case** keys.
- When invoking a delegate, the host binds arguments by `parameterName.ToSnakeLower()`.
- Tool names are sent to the model as `snake_case` (for example `GetOrderStatus` becomes `get_order_status`).
- You can optionally accept injected parameters:
  - `IFunctionContext` is injected by type,
  - `CancellationToken` is injected by type.

Tip: Prefer named methods over lambdas for tools. Some compiler-generated lambda names are not stable/public-friendly and may fail normalization. If you need full control, register a `ChatFunction` with an explicit `Name`.

### Tool schema generation (what the model sees)

In the ChatAIze stack, schema generation works like this:

- If `IChatFunction.Parameters` is non-null, the host uses it as the JSON schema.
- Otherwise, the host derives a schema from the delegate signature via reflection.

Important note:

- The schema serializer excludes `CancellationToken` automatically.
- It currently **does not** exclude `IFunctionContext`.

If your callback accepts `IFunctionContext`, provide an explicit parameter list to avoid showing `context` as a user-supplied tool input:

```csharp
using ChatAIze.PluginApi;

var function = new ChatFunction
{
    Name = "get_order_status",
    Description = "Gets the status of an order by id.",
    Callback = GetOrderStatusAsync,
    Parameters =
    [
        new FunctionParameter(typeof(string), "orderId", "Order id shown to the customer.", isRequired: true),
    ]
};

plugin.AddFunction(function);
```

If you rely on reflection-based schemas, you can improve the model-visible documentation/constraints using:

- `DescriptionAttribute` on the method and/or parameters
- string data annotations such as `[Required]`, `[MinLength]`, `[MaxLength]`, `[StringLength]`

Example (reflection schema + runtime validation for strings):

```csharp
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ChatAIze.Abstractions.Chat;

[Description("Searches the knowledge base.")]
private static async Task<string> SearchAsync(
    IFunctionContext context,
    [Description("Search query.")] [Required] [MinLength(2)] string query,
    CancellationToken cancellationToken = default)
{
    var result = await context.SearchKnowledgeAsync(query, cancellationToken: cancellationToken);
    return result.ToString();
}
```

### Error handling conventions

- For **recoverable** failures, prefer returning a string that starts with `"Error: "`.
- In the OpenAI provider, a tool call is considered successful only when the returned string does **not** start with `"Error:"` (case-insensitive).
- Exceptions thrown by your tool delegate are not swallowed by the binder; they can bubble up and disrupt the completion. Catch exceptions and return a user-friendly `"Error: ..."` instead.
- If your delegate returns a non-string value, the host serializes it to JSON for tool output using snake_case property names.

### “Double check” tools

Set `IChatFunction.RequiresDoubleCheck = true` to require an extra round-trip where the model is asked to call the tool again to confirm intent.
This behavior is implemented in both the OpenAI and Gemini providers.

## Settings (Plugin Configuration UI)

Plugins can expose settings that admins configure in the dashboard.

In ChatAIze.Chatbot, your `SettingsCallback` is invoked while rendering the plugin settings page and when settings change. Keep it fast and return deterministic ids so the host can diff and cache settings trees.

### Settings tree: containers vs leaf settings

- Containers: `SettingsSection`, `SettingsGroup`, `SettingsParagraph` (layout-only, do not store a value).
- Leaf settings: `StringSetting`, `SelectionSetting`, `IntegerSetting`, `BooleanSetting`, `DecimalSetting`, `DateTimeSetting`, `ListSetting`, `MapSetting` (store a value under `ISetting.Id`).

### Settings cheat sheet

| Setting | Stored JSON kind | Typical use |
| --- | --- | --- |
| `StringSetting` | string | API keys, URLs, names |
| `IntegerSetting` / `DecimalSetting` | number | thresholds, limits |
| `BooleanSetting` | true/false | feature toggles |
| `SelectionSetting` | string | “pick one” choices |
| `DateTimeSetting` | string (date/time) | schedules, reminders |
| `ListSetting` | array of strings | tags, allow/deny lists |
| `MapSetting` | object (string → string) | headers, simple key/value configs |

### Example settings UI

```csharp
using ChatAIze.Abstractions.Settings;
using ChatAIze.Abstractions.UI;
using ChatAIze.PluginApi.Settings;

plugin.SettingsCallback = _ => ValueTask.FromResult<IReadOnlyCollection<ISetting>>(
[
    new SettingsSection(
        id: "com.mycompany.myplugin:section.general",
        title: "General",
        description: "Configuration for My Plugin.",
        settings:
        [
            new StringSetting(
                id: "com.mycompany.myplugin:api_key",
                title: "API key",
                description: "Used to call the Example API.",
                textFieldType: TextFieldType.Password,
                maxLength: 200),

            new SelectionSetting(
                id: "com.mycompany.myplugin:region",
                title: "Region",
                defaultValue: "us",
                style: SelectionSettingStyle.Automatic,
                choices:
                [
                    new SelectionChoice(value: "us", title: "United States"),
                    new SelectionChoice(value: "eu", title: "Europe"),
                ])
        ])
]);
```

Notes about ChatAIze.Chatbot UI behavior:

- `SelectionSettingStyle.Automatic` chooses a UI based on the number of choices (≤ 3 segmented, ≤ 6 radio buttons, otherwise a dropdown).
- `SettingsButton` callbacks are executed on the server when clicked; the provided `CancellationToken` is canceled when the settings view is disposed.

### List settings (`ListSetting`)

Use `ListSetting` when you want the admin to enter a **list of strings** (tags, keywords, allowed domains, etc.).

- Stored as a JSON array of strings under `ISetting.Id` (e.g. `["a", "b", "c"]`).
- In ChatAIze.Chatbot, the UI is a simple list editor with one text field per item.
- You still need to validate values at runtime (treat settings as untrusted input).

Example:

```csharp
using ChatAIze.PluginApi.Settings;

var allowedDomains = new ListSetting(
    id: "com.mycompany.myplugin:allowed_domains",
    title: "Allowed email domains",
    description: "If set, only users with these email domains are allowed to run certain tools.",
    itemPlaceholder: "@example.com",
    maxItems: 20,
    maxItemLength: 100,
    allowDuplicates: false,
    isLowercase: true);
```

Reading a list setting:

```csharp
var domains = await context.Settings.GetAsync(
    "com.mycompany.myplugin:allowed_domains",
    defaultValue: new List<string>(),
    cancellationToken);
```

Notes:

- `AllowDuplicates` and `IsLowercase` are **UI hints**; host enforcement can vary. Even if you set `allowDuplicates: false`, still handle duplicates defensively.
- If you need a list of non-strings (numbers/objects), you currently have to parse strings yourself or expose dedicated settings (e.g. `IntegerSetting` + `ListSetting`).

### Map settings (`MapSetting`)

Use `MapSetting` when you want the admin to enter **key/value pairs of strings** (headers, labels → URLs, product → price strings, etc.).

- Stored as a JSON object under `ISetting.Id` (e.g. `{"key":"value"}`).
- In ChatAIze.Chatbot, the UI is a list of “Key” + “Value” fields and is serialized as `Dictionary<string, string>`.
- Keys are treated as plain strings (no normalization). Validate and normalize if your use case requires it (for example use case-insensitive keys for HTTP headers).

Example:

```csharp
using ChatAIze.PluginApi.Settings;

var defaultHeaders = new MapSetting(
    id: "com.mycompany.myplugin:default_headers",
    title: "Default HTTP headers",
    description: "Sent with every outbound request made by this plugin.",
    keyPlaceholder: "Header-Name",
    valuePlaceholder: "Value",
    maxItems: 30,
    maxKeyLength: 100,
    maxValueLength: 500);
```

Reading a map setting:

```csharp
var headers = await context.Settings.GetAsync(
    "com.mycompany.myplugin:default_headers",
    defaultValue: new Dictionary<string, string>(),
    cancellationToken);

// Optional: treat keys case-insensitively for HTTP header usage
var headersNormalized = new Dictionary<string, string>(headers, StringComparer.OrdinalIgnoreCase);
```

Notes:

- `MapSetting` is string → string. If you want structured values, store JSON in the string values and parse it yourself (and validate carefully), or expose dedicated typed settings.
- Host UIs may handle duplicate keys differently. In ChatAIze.Chatbot, duplicate keys are de-duplicated on save.

### Reading settings at runtime

Plugin settings are stored by the host as JSON keyed by `ISetting.Id` and exposed through `IPluginSettings`:

```csharp
var apiKey = await context.Settings.GetAsync("com.mycompany.myplugin:api_key", "", cancellationToken);
```

Security note: use plugin settings for secrets (API keys, tokens) and avoid hardcoding credentials in your plugin binary or source code. Do not log secret values.

## Workflow Actions (Integration Function Steps)

ChatAIze.Chatbot supports “integration functions” configured in the dashboard. These are workflows composed of action steps and optional conditions.

Your plugin can contribute reusable actions via `IFunctionAction` / `FunctionAction`.

### Define an action with settings and placeholders

```csharp
using ChatAIze.Abstractions.Chat;
using ChatAIze.PluginApi;
using ChatAIze.PluginApi.Settings;

var action = new FunctionAction(
    id: "com.mycompany.myplugin:actions.create_ticket",
    title: "Create Ticket",
    callback: CreateTicketAsync)
{
    Description = "Creates a ticket and exposes it as {ticket}.",
    Placeholders = ["ticket"]
};

action.AddStringSetting("subject", title: "Subject", maxLength: 200);
action.AddStringSetting("message", title: "Message", editorLines: 4, maxLength: 2000);

plugin.AddAction(action);

static async Task<string> CreateTicketAsync(IActionContext context, string subject, string message, CancellationToken cancellationToken)
{
    if (context.IsPreview)
    {
        context.SetPlaceholder("ticket", new { id = 123, url = "https://example.test/tickets/123" });
        return "OK: (preview) ticket created.";
    }

    // TODO: create the ticket in your external system here.
    context.SetPlaceholder("ticket", new { id = 123, url = "https://example.com/tickets/123" });
    return "Ticket created.";
}
```

### List/map settings in actions

Actions can use `ListSetting` and `MapSetting` as step configuration. This is useful for things like:

- tags/keywords (list),
- HTTP headers (map),
- variable substitution tables (map),
- allow/deny lists (list).

If you want the host to bind settings into your delegate parameters, keep the setting ids compatible with C# parameter names (e.g. `headers`, `tags`).

```csharp
var action = new FunctionAction(
    id: "com.mycompany.myplugin:actions.send_request",
    title: "Send Request",
    callback: SendRequestAsync);

action.AddStringSetting("url", title: "URL");
action.AddMapSetting("headers", title: "Headers", keyPlaceholder: "Header", valuePlaceholder: "Value");
action.AddListSetting("tags", title: "Tags", itemPlaceholder: "tag");

plugin.AddAction(action);

static async Task<string> SendRequestAsync(
    IActionContext context,
    string url,
    Dictionary<string, string> headers,
    List<string> tags,
    CancellationToken cancellationToken)
{
    // Use headers/tags here...
    return $"OK: sending to {url} with {headers.Count} headers and {tags.Count} tags.";
}
```

In ChatAIze.Chatbot, placeholders are expanded in action settings before invocation. For list/map values (JSON arrays/objects), substitution happens on the raw JSON text and is reparsed, so placeholders must produce valid JSON after replacement.

### Action binding rules

In ChatAIze.Chatbot:

- Action settings are passed as a JSON dictionary keyed by your setting ids.
- Delegates can accept `IActionContext` and/or `CancellationToken` injected by type.
- Other parameters are bound by **exact name** or **snake_case name**.

### Placeholder expansion in action settings

Before calling your action callback, the host expands placeholders in the action settings:

1. placeholders from integration-function parameters (e.g. `{order_id}`, `{customer_email}`),
2. placeholders produced by previous actions (e.g. `{ticket.id}`, `{ticket.url}`).

Substitution is plain text. For JSON objects/arrays, ChatAIze.Chatbot performs substitution on the raw JSON and reparses it, so replacement must produce valid JSON.

ChatAIze.Chatbot normalizes placeholder ids to `snake_case` and may suffix them (`_2`, `_3`, …) to avoid collisions when multiple actions in a workflow declare the same placeholder id. Use the placeholder names shown in the dashboard for the specific action placement.

### Action success/failure

- Returning `"Error: ..."` does **not** automatically mark an action as failed.
- To fail an action intentionally, call `context.SetActionResult(false, "reason")`.
- Unhandled exceptions are caught by the workflow runner and recorded as an action failure; in non-preview runs the message may be replaced by a generic one.

### Dynamic action settings (conditional fields)

You can render different settings based on the current placement values via `SettingsCallback`:

```csharp
using System.Text.Json;
using ChatAIze.Abstractions.Settings;
using ChatAIze.PluginApi;
using ChatAIze.PluginApi.Settings;
using ChatAIze.Utilities.Extensions;

action.SettingsCallback = (values) =>
{
    var settings = new List<ISetting>();

    settings.AddSelectionSetting(
        id: "mode",
        title: "Mode",
        defaultValue: "simple",
        choices:
        [
            new SelectionChoice("simple", "Simple"),
            new SelectionChoice("advanced", "Advanced"),
        ]);

    var mode = values.TryGetSettingValue("mode", "simple");

    if (mode == "advanced")
    {
        settings.AddIntegerSetting(id: "retries", title: "Retries", defaultValue: 3, minValue: 0, maxValue: 10);
    }

    return settings;
};
```

## Workflow Conditions (Gates)

Conditions run before an integration function executes and decide whether it’s allowed.

```csharp
using ChatAIze.Abstractions.Chat;
using ChatAIze.PluginApi;
using ChatAIze.PluginApi.Settings;

var condition = new FunctionCondition(
    id: "com.mycompany.myplugin:conditions.company_email",
    title: "Company email required",
    callback: (IConditionContext context, string domain) =>
        context.User.Email?.EndsWith(domain, StringComparison.OrdinalIgnoreCase) == true
            ? true
            : $"Only {domain} users are allowed.");

condition.AddStringSetting("domain", title: "Email domain", placeholder: "@mycompany.com");
plugin.AddCondition(condition);
```

Conditions can also use `ListSetting` / `MapSetting` for configuration (for example: a list of allowed domains or a map of role → allowed).

In ChatAIze.Chatbot:

- Condition settings are placeholder-expanded from the integration function parameters before evaluation.
- Return conventions:
  - `true` → allow
  - `false` → deny (no reason)
  - string/other value → deny with a reason (non-string values are JSON-serialized)

Note: conditions run before actions, so they do not have access to action placeholders (only to function parameters and chat/user context).

## Useful Patterns

### Status and progress

You can update the current execution status (and optional progress 0–100) via `IFunctionContext.SetStatus`:

```csharp
context.SetStatus("Calling external API...", progress: 30);
// ...
context.SetStatus("Done.", progress: 100);
```

### Logging

All context types expose `Log(...)` (wired to the host logging pipeline):

```csharp
using Microsoft.Extensions.Logging;

context.Log(LogLevel.Information, "Starting order lookup...");
```

Tip: avoid logging secrets. In ChatAIze.Chatbot, logs can be visible to administrators.

### Forms and confirmations

Plugins can prompt the current user with built-in UI dialogs (when supported by the host):

```csharp
var ok = await context.ShowConfirmationAsync(
    title: "Delete account",
    message: "Are you sure you want to delete your account?",
    yesText: "Delete",
    noText: "Cancel",
    cancellationToken: cancellationToken);

if (!ok)
{
    return "Error: Cancelled by user.";
}
```

### Quick replies

Quick replies are suggested “chips” in the chat UI. A tool or action can update them via `IFunctionContext.QuickReplies`:

```csharp
using ChatAIze.PluginApi;

context.QuickReplies.Clear();
context.QuickReplies.Add(new QuickReply("Order", "Where is my order?"));
context.QuickReplies.Add(new QuickReply("Refund", "I want a refund"));
```

### Knowledge search and document retrieval

```csharp
var result = await context.SearchKnowledgeAsync("refund policy", folder: "Support", cancellationToken: cancellationToken);
var content = await context.GetDocumentContentAsync("Refund Policy", cancellationToken);
```

### Per-user storage

```csharp
var lastOrderId = await context.User.GetPropertyAsync("com.mycompany.myplugin:last_order_id", "", cancellationToken);
await context.User.SetPropertyAsync("com.mycompany.myplugin:last_order_id", "12345", cancellationToken);
```

### Optional plugin-to-plugin integration

If your plugin can optionally integrate with another plugin, you can ask the host for a plugin instance by type:

```csharp
var other = context.GetPlugin<OtherPluginType>(id: "com.other.plugin");
if (other is not null)
{
    // Use the optional integration.
}
```

Avoid hard dependencies on other plugins being present; always handle `null`.

### Custom databases

Plugins can use `IDatabaseManager` via `context.Databases`:

```csharp
using ChatAIze.PluginApi.Databases;
using ChatAIze.Abstractions.Databases.Enums;

var db = await context.Databases.FindDatabaseByTitleAsync("Orders", cancellationToken);
if (db is null)
{
    return "Error: Database 'Orders' not found.";
}

var item = await context.Databases.GetFirstItemAsync(
    db,
    sorting: new DatabaseSorting(property: "created_at", order: SortOrder.Descending),
    cancellationToken: cancellationToken,
    filters:
    [
        new DatabaseFilter(property: "order_id", type: FilterType.Equals, value: orderId, options: FilterOptions.None)
    ]);
```

## Troubleshooting

### “My plugin loads but my tool never runs”

- In ChatAIze.Chatbot, ensure Integrations are enabled in the dashboard settings (tools and integration functions are added only when integrations are enabled).
- Ensure your tool name is unique across all plugins and integration functions.
- Ensure `IChatFunction.Callback` is non-null (tools without callbacks are treated as integration functions and executed by the host’s default callback).
- Prefer named methods over lambdas for `AddFunction(Delegate)`; compiler-generated names can be unstable.

### “It works locally but fails when uploaded”

- Dashboard upload uploads one `.dll` file. If you use additional dependencies, deploy by copying your full output folder to `plugins/`.
- Check that the `.deps.json` and dependency `.dll` files are present.

### “My action returns an error string but the workflow still says it succeeded”

- Actions are marked success/failure via `IActionContext.SetActionResult(...)`.
- Returning `"Error: ..."` is just a string result; it does not automatically fail the action.

## Examples

- `ChatAIze.PluginApi.ExamplePlugin/MyShop.cs` demonstrates:
  - settings (leaf + containers)
  - a tool (`AddFunction`)
  - an action (`FunctionAction`)
  - a condition (`FunctionCondition`)
