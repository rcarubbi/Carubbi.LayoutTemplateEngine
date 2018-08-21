using Carubbi.LayoutTemplateEngine.Interfaces;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;

namespace Carubbi.LayoutTemplateEngine.Implementations
{
    /// <inheritdoc />
    /// <summary>
    /// Gerenciador de templates baseado na linguagem Razor
    /// </summary>
    public class RazorEngine : ILayoutTemplateEngine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string RenderFromContentTemplate(string content, IDictionary<string, object> data)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        /// <summary>
        /// Renderiza um template a partir do nome, substituindo as variáveis do template pelas variáveis passadas como parâmetro
        /// </summary>
        /// <param name="templateName">Nome do Template</param>
        /// <param name="data">Dicionário com as váriáveis (Chave/Valor)</param>
        /// <returns>Template com as variávies substituídas</returns>
        public string RenderTemplate(string templateName, IDictionary<string, object> data)
        {
            DynamicViewBag viewBag = new DynamicViewBag();
            viewBag.AddDictionaryValuesEx(data);
            var result = Razor.Parse(templateName, null, viewBag, null);
            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Renderiza um template a partir do nome utilizando masterPage, substituindo as variáveis do template pelas variáveis passadas como parâmetro
        /// </summary>
        /// <param name="masterPage">Nome da master page</param>
        /// <param name="templateName">Nome do Template</param>
        /// <param name="data">Dicionário com as váriáveis (Chave/Valor)</param>
        /// <returns>Template com as variávies substituídas</returns>
        public string RenderTemplate(string masterPage, string templateName, IDictionary<string, object> data)
        {
            return RenderTemplate(templateName, data);
        }
    }   
}
