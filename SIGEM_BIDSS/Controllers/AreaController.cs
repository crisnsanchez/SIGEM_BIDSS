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
    public class AreaController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Area
        public ActionResult Index()
        {
            return View(db.tbArea.ToList());
        }

        // GET: Area/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArea tbArea = db.tbArea.Find(id);
            if (tbArea == null)
            {
                return HttpNotFound();
            }
            return View(tbArea);
        }

        // GET: Area/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Area/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "are_Id,are_Descripcion,are_UsuarioCrea,are_FechaCrea,are_UsuarioModifica,are_FechaModifica")] tbArea tbArea)
        {
            if (ModelState.IsValid)
            {
                db.tbArea.Add(tbArea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbArea);
        }

        // GET: Area/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArea tbArea = db.tbArea.Find(id);
            if (tbArea == null)
            {
                return HttpNotFound();
            }
            return View(tbArea);
        }

        // POST: Area/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "are_Id,are_Descripcion,are_UsuarioCrea,are_FechaCrea,are_UsuarioModifica,are_FechaModifica")] tbArea tbArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbArea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbArea);
        }

        // GET: Area/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArea tbArea = db.tbArea.Find(id);
            if (tbArea == null)
            {
                return HttpNotFound();
            }
            return View(tbArea);
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbArea tbArea = db.tbArea.Find(id);
            db.tbArea.Remove(tbArea);
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
