namespace Carubbi.LayoutTemplateEngine.Interfaces;

/// <summary>
/// Base implementation of <see cref="ILayoutTemplateEngine"/> for engines that load
/// templates by file path and compose master pages by injecting the rendered child
/// into the master page as the <c>TemplateBody</c> variable.
/// </summary>
public abstract class LayoutTemplateEngineBase : ILayoutTemplateEngine
{
    public virtual string RenderTemplate(string templateName, IDictionary<string, object> data) =>
        RenderFromContentTemplate(File.ReadAllText(templateName), data);

    public virtual string RenderTemplate(string masterPage, string templateName, IDictionary<string, object> data)
    {
        var body = RenderTemplate(templateName, data);
        var merged = new Dictionary<string, object>(data)
        {
            ["TemplateBody"] = body
        };
        return RenderTemplate(masterPage, merged);
    }

    public abstract string RenderFromContentTemplate(string content, IDictionary<string, object> data);
}
