namespace Carubbi.LayoutTemplateEngine.Interfaces;

/// <summary>
/// Manages layout template rendering, abstracting over concrete template engines such as Razor or NVelocity.
/// </summary>
public interface ILayoutTemplateEngine
{
    /// <summary>
    /// Renders a template by name, substituting each template variable with the matching value in <paramref name="data"/>.
    /// </summary>
    /// <param name="templateName">The template name.</param>
    /// <param name="data">The variable dictionary (key/value).</param>
    /// <returns>The rendered template.</returns>
    string RenderTemplate(string templateName, IDictionary<string, object> data);

    /// <summary>
    /// Renders a template by name using a master/layout page, substituting the template variables.
    /// </summary>
    /// <param name="masterPage">The master page name.</param>
    /// <param name="templateName">The template name.</param>
    /// <param name="data">The variable dictionary (key/value).</param>
    /// <returns>The rendered template.</returns>
    string RenderTemplate(string masterPage, string templateName, IDictionary<string, object> data);

    /// <summary>
    /// Renders a template from its raw content string, substituting the template variables.
    /// </summary>
    /// <param name="content">The raw template content.</param>
    /// <param name="data">The variable dictionary (key/value).</param>
    /// <returns>The rendered template.</returns>
    string RenderFromContentTemplate(string content, IDictionary<string, object> data);
}
