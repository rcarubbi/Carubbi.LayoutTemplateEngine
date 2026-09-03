using Carubbi.LayoutTemplateEngine.Interfaces;
using RazorEngineCore;

namespace Carubbi.LayoutTemplateEngine.Razor;

public class RazorEngine : LayoutTemplateEngineBase
{
    private readonly IRazorEngine _razorEngine = new RazorEngineCore.RazorEngine();

    public override string RenderFromContentTemplate(string content, IDictionary<string, object> data)
    {
        var template = _razorEngine.Compile(content);
        return template.Run(data);
    }
}
