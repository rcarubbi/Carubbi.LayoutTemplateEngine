using Carubbi.LayoutTemplateEngine.Interfaces;
using Commons.Collections;
using NVelocity;
using NVelocity.App;

namespace Carubbi.LayoutTemplateEngine.NVelocity;

public class NVelocityEngine : ILayoutTemplateEngine
{
    private readonly VelocityEngine _velocityEngine;

    public NVelocityEngine(string templatesPath)
    {
        var properties = new ExtendedProperties();
        properties.SetProperty("resource.loader", "file");
        properties.SetProperty("file.resource.loader.class", "NVelocity.Runtime.Resource.Loader.FileResourceLoader");
        properties.SetProperty("file.resource.loader.path", Path.GetFullPath(templatesPath));
        _velocityEngine = new VelocityEngine();
        _velocityEngine.Init(properties);
    }

    public string RenderTemplate(string templateName, IDictionary<string, object> data)
    {
        var template = _velocityEngine.GetTemplate(templateName);
        using var writer = new StringWriter();
        var context = BuildContext(data);
        template.Merge(context, writer);
        return writer.ToString();
    }

    public string RenderTemplate(string masterPage, string templateName, IDictionary<string, object> data)
    {
        var body = RenderTemplate(templateName, data);
        var merged = new Dictionary<string, object>(data)
        {
            [TemplateBodyKey] = body
        };
        return RenderTemplate(masterPage, merged);
    }

    public string RenderFromContentTemplate(string content, IDictionary<string, object> data)
    {
        using var writer = new StringWriter();
        var context = BuildContext(data);
        _velocityEngine.Evaluate(context, writer, "ContentTemplate", new StringReader(content));
        return writer.ToString();
    }

    private static VelocityContext BuildContext(IDictionary<string, object> data)
    {
        var context = new VelocityContext();
        foreach (var (key, value) in data)
        {
            context.Put(key, value);
        }
        return context;
    }

    private const string TemplateBodyKey = "TemplateBody";
}
