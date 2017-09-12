using MCART.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabMedWeb.Controllers
{
    [Name("Inicio")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Muestra la página de inicio.
        /// </summary>
        /// <returns>GET /</returns>
        public ActionResult Index() => View();
    }
}