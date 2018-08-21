using Carubbi.LayoutTemplateEngine.Interfaces;
using RazorEngine;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.IO;

namespace Carubbi.LayoutTemplateEngine.Razor3
{
    public class Razor3Engine : ILayoutTemplateEngine
    {
        public string RenderFromContentTemplate(string content, IDictionary<string, object> data)
        {

            Engine.Razor.AddTemplate("template", content);
            // On startup
            Engine.Razor.Compile("template", null);
            // instead of the Razor.Parse call
            var viewBag = new DynamicViewBag();
            viewBag.AddDictionary(data);

            var result = Engine.Razor.Run("template", null, viewBag);
            return result;
        }

        public string RenderTemplate(string templateName, IDictionary<string, object> data)
        {
            var templateContent = File.ReadAllText(templateName);
            Engine.Razor.AddTemplate(templateName, templateContent);
           
            // On startup
            Engine.Razor.Compile(templateName, null);
          
            // instead of the Razor.Parse call
            var viewBag = new DynamicViewBag();
            viewBag.AddDictionary(data);

            var result = Engine.Razor.Run(templateName, null, viewBag);
            return result;
        }

   
        public string RenderTemplate(string masterPage, string templateName, IDictionary<string, object> data)
        {
            var masterContent = File.ReadAllText(masterPage);
            Engine.Razor.AddTemplate(masterPage, masterContent);
            return RenderTemplate(templateName, data);
        }
    }
}
