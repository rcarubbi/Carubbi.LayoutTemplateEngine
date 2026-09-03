# Carubbi.LayoutTemplateEngine

Layout engine wrappers for Razor and NVelocity, plus a shared abstraction.

> An abstraction over layout template engines like Razor or NVelocity. This kind of component is useful to create email templates, for example. The implementations can be injected using any dependency injection container.

## Projects

| Project | Package | Engine |
|---------|---------|--------|
| `Carubbi.LayoutTemplateEngine.Interfaces` | `Carubbi.LayoutTemplateEngine.Interfaces` | Shared `ILayoutTemplateEngine` abstraction |
| `Carubbi.LayoutTemplateEngine` | `Carubbi.LayoutTemplateEngine` | Razor via [RazorEngineCore](https://github.com/adoconnection/RazorEngineCore) |
| `Carubbi.LayoutTemplateEngine.NVelocity` | `Carubbi.LayoutTemplateEngine.NVelocity` | [NVelocity](http://www.castleproject.org/) |
| `Carubbi.LayoutTemplateEngine.Razor3` | `Carubbi.LayoutTemplateEngine.Razor3` | Razor via RazorEngineCore (master-page aware) |

Target framework: `net10.0`. Requires .NET 10 SDK.

## Usage

All engines implement `Carubbi.LayoutTemplateEngine.Interfaces.ILayoutTemplateEngine`:

```csharp
public interface ILayoutTemplateEngine
{
    string RenderTemplate(string templateName, IDictionary<string, object> data);
    string RenderTemplate(string masterPage, string templateName, IDictionary<string, object> data);
    string RenderFromContentTemplate(string content, IDictionary<string, object> data);
}
```

### Razor

```csharp
var engine = new Carubbi.LayoutTemplateEngine.RazorEngine();
var data = new Dictionary<string, object> { ["Name"] = "World" };

var result = engine.RenderFromContentTemplate("Hello @Model[\"Name\"]!", data);
// "Hello World!"

var fromFile = engine.RenderTemplate("C:\\templates\\email.cshtml", data);
```

Master-page rendering embeds the child template output as `@Model["TemplateBody"]`:

```csharp
var html = engine.RenderTemplate("C:\\templates\\layout.cshtml", "C:\\templates\\content.cshtml", data);
// layout.cshtml: <html><body>@Model["TemplateBody"]</body></html>
```

### Razor3

Same API as the Razor engine, in its own package (`Carubbi.LayoutTemplateEngine.Razor3.Razor3Engine`).

### NVelocity

Templates are resolved by name within a templates directory. A `VelocityContext` is populated from the data dictionary.

```csharp
var engine = new Carubbi.LayoutTemplateEngine.NVelocity.NVelocityEngine(@"C:\templates");
var data = new Dictionary<string, object> { ["Name"] = "World" };

var result = engine.RenderFromContentTemplate("Hello $Name!", data);
// "Hello World!"
```

```csharp
// content.vm:  Greetings $Name
// master.vm:   <html><body>$TemplateBody</body></html>
var html = engine.RenderTemplate("master.vm", "content.vm", data);
// "<html><body>Greetings World</body></html>"
```

## NuGet

```bash
dotnet add package Carubbi.LayoutTemplateEngine
dotnet add package Carubbi.LayoutTemplateEngine.NVelocity
dotnet add package Carubbi.LayoutTemplateEngine.Razor3
dotnet add package Carubbi.LayoutTemplateEngine.Interfaces
```

## License

MIT
