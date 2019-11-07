using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class DepartamentoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Departamento
        public ActionResult Index()
        {
            return View(db.tbDepartamento.ToList());
        }

        // GET: Departamento/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // GET: Departamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departamento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            if (ModelState.IsValid)
            {
                db.tbDepartamento.Add(tbDepartamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbDepartamento);
        }

        // GET: Departamento/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // POST: Departamento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dep_Codigo,dep_Nombre,dep_UsuarioCrea,dep_FechaCrea,dep_UsuarioModifica,dep_FechaModifica")] tbDepartamento tbDepartamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDepartamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbDepartamento);
        }

        // GET: Departamento/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return HttpNotFound();
            }
            return View(tbDepartamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tbDepartamento tbDepartamento = db.tbDepartamento.Find(id);
            db.tbDepartamento.Remove(tbDepartamento);
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
