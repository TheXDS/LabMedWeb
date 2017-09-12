using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabMedWeb.Models;
using MCART.Attributes;

namespace LabMedWeb.Controllers
{
    [Name("Grupos de cuentas")]
    public class CuentaGroupsController : Controller
    {
        private LabMedWebContext db = new LabMedWebContext();

        // GET: CuentaGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.CuentaGroups.ToListAsync());
        }

        // GET: CuentaGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaGroup cuentaGroup = await db.CuentaGroups.FindAsync(id);
            if (cuentaGroup == null)
            {
                return HttpNotFound();
            }
            return View(cuentaGroup);
        }

        // GET: CuentaGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuentaGroups/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] CuentaGroup cuentaGroup)
        {
            if (ModelState.IsValid)
            {
                db.CuentaGroups.Add(cuentaGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cuentaGroup);
        }

        // GET: CuentaGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaGroup cuentaGroup = await db.CuentaGroups.FindAsync(id);
            if (cuentaGroup == null)
            {
                return HttpNotFound();
            }
            return View(cuentaGroup);
        }

        // POST: CuentaGroups/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] CuentaGroup cuentaGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentaGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cuentaGroup);
        }

        // GET: CuentaGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaGroup cuentaGroup = await db.CuentaGroups.FindAsync(id);
            if (cuentaGroup == null)
            {
                return HttpNotFound();
            }
            return View(cuentaGroup);
        }

        // POST: CuentaGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CuentaGroup cuentaGroup = await db.CuentaGroups.FindAsync(id);
            db.CuentaGroups.Remove(cuentaGroup);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
