using Carubbi.LayoutTemplateEngine.Interfaces;
using RazorEngineCore;

namespace Carubbi.LayoutTemplateEngine;

public class RazorEngine : ILayoutTemplateEngine
{
    private readonly IRazorEngine _razorEngine = new RazorEngineCore.RazorEngine();

    public string RenderTemplate(string templateName, IDictionary<string, object> data) =>
        RenderFromContentTemplate(File.ReadAllText(templateName), data);

    public string RenderTemplate(string masterPage, string templateName, IDictionary<string, object> data)
    {
        var body = RenderTemplate(templateName, data);
        var merged = new Dictionary<string, object>(data)
        {
            [TemplateBodyKey] = body
        };
        return RenderFromContentTemplate(File.ReadAllText(masterPage), merged);
    }

    public string RenderFromContentTemplate(string content, IDictionary<string, object> data)
    {
        var template = _razorEngine.Compile(content);
        return template.Run(data);
    }

    private const string TemplateBodyKey = "TemplateBody";
}
