using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class ProfesionalesController : Controller
    {
        private TurnosContext db = new TurnosContext();

        // GET: Profesionales
        public ActionResult Index()
        {
            var profesionales = db.Profesionales.Include(p => p.Empresa);
            return View(profesionales.ToList());
        }

        // GET: Profesionales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesionales.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            return View(profesional);
        }

        // GET: Profesionales/Create
        public ActionResult Create()
        {
            ViewBag.NitEmpresa = new SelectList(db.Empresas, "NitEmpresa", "Nombre");
            return View();
        }

        // POST: Profesionales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfesionalID,Nombre,Imagen,NitEmpresa")] Profesional profesional)
        {
            if (ModelState.IsValid)
            {
                db.Profesionales.Add(profesional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NitEmpresa = new SelectList(db.Empresas, "NitEmpresa", "Nombre", profesional.NitEmpresa);
            return View(profesional);
        }

        // GET: Profesionales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesionales.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            ViewBag.NitEmpresa = new SelectList(db.Empresas, "NitEmpresa", "Nombre", profesional.NitEmpresa);
            return View(profesional);
        }

        // POST: Profesionales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfesionalID,Nombre,Imagen,NitEmpresa")] Profesional profesional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NitEmpresa = new SelectList(db.Empresas, "NitEmpresa", "Nombre", profesional.NitEmpresa);
            return View(profesional);
        }

        // GET: Profesionales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesional profesional = db.Profesionales.Find(id);
            if (profesional == null)
            {
                return HttpNotFound();
            }
            return View(profesional);
        }

        // POST: Profesionales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesional profesional = db.Profesionales.Find(id);
            db.Profesionales.Remove(profesional);
            db.SaveChanges();
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
