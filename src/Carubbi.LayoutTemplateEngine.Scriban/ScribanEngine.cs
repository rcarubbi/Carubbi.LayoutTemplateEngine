using Carubbi.LayoutTemplateEngine.Interfaces;
using Scriban;
using Scriban.Runtime;

namespace Carubbi.LayoutTemplateEngine.Scriban;

public class ScribanEngine : LayoutTemplateEngineBase
{
    public override string RenderFromContentTemplate(string content, IDictionary<string, object> data)
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
}
