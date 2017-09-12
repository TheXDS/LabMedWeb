using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabMedWeb.Models;
using LabMedWeb.Logic;
using MCART.Attributes;

namespace LabMedWeb.Controllers
{
    [Name(" /!\\ Pruebas de desarrollo")]

    public class TestController : Controller
    {
        //Contexto de la base de datos
        private LabMedWebContext db = new LabMedWebContext();


        // GET: Test
        public ActionResult Index()
        {
            return View("Nope");
        }

        //Obtiene una lista en json de categorias.
        public ActionResult GetSubCat(long id)
            => Json(db.Categorias.Where((p) => p.Parent.ID == id),JsonRequestBehavior.AllowGet);

        //Obtiene una lista en json de categorias.
        public ActionResult GetSubCatParented(long id)
        {
            List<Categoria> outp = new List<Categoria>();
            foreach (var j in db.Categorias)
                if (j.Parent?.ID == id) outp.Add(j);
            return Json(outp.ToArray(),JsonRequestBehavior.AllowGet);

        }



        //Obtiene una lista en json de cuentas dentro de una categoría.
        public ActionResult GetSubCuentas(long id)
            => Json(db.Cuentas.Where((p) => p.Parent.ID == id),JsonRequestBehavior.AllowGet);
    }
}