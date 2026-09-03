using Carubbi.LayoutTemplateEngine.Interfaces;
using Scriban;
using Scriban.Runtime;

namespace Carubbi.LayoutTemplateEngine.Scriban;

public class ScribanEngine : ILayoutTemplateEngine
{
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
        var template = Template.Parse(content);
        return template.Render(BuildModel(data));
    }

    private static ScriptObject BuildModel(IDictionary<string, object> data)
    {
        var model = new ScriptObject();
        foreach (var (key, value) in data)
        {
            model[key] = value;
        }
        return model;
    }

    private const string TemplateBodyKey = "TemplateBody";
}
