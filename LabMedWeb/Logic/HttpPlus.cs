using LabMedWeb.Models;
using MCART.Attributes;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using static MCART.Objects;

namespace LabMedWeb.Logic
{
    /// <summary>
    /// Contiene helpers que ayudan a generar porciones especiales para las
    /// vistas.
    /// </summary>
    public static class HttpPlus
    {
        /// <summary>
        /// Obtiene un menú de todos los <see cref="Controller"/> disponibles 
        /// en la aplicación.
        /// </summary>
        /// <param name="that">Página que requiere el menú.</param>
        /// <returns>Un iterador con nuevos elementos de menú para una página.</returns>
        public static IEnumerable<MvcHtmlString> MainMenu(this WebViewPage<dynamic> that, object HtmlAttrs = null)
        {
            foreach (Type j in typeof(LabMedWebContext).Assembly.GetTypes())
            {
                if (typeof(Controller).IsAssignableFrom(j))
                {
                    yield return that.Html.ActionLink(
                        j.GetAttr<NameAttribute>()?.Value ?? $" /!\\ {j.Name}",
                        "Index",
                        j.Name.Replace("Controller", string.Empty), null, HtmlAttrs);
                }
            }

        }
    }
}