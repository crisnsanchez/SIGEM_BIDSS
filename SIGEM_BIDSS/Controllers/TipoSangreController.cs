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
    public class TipoSangreController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: TipoSangre
        public ActionResult Index()
        {
            return View(db.tbTipoSangre.ToList());
        }

        // GET: TipoSangre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSangre tbTipoSangre = db.tbTipoSangre.Find(id);
            if (tbTipoSangre == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSangre);
        }

        // GET: TipoSangre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoSangre/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tps_Id,tps_nombre,tps_UsuarioCrea,tps_FechaCrea,tps_UsuarioModifica,tps_FechaModifica")] tbTipoSangre tbTipoSangre)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoSangre.Add(tbTipoSangre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTipoSangre);
        }

        // GET: TipoSangre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSangre tbTipoSangre = db.tbTipoSangre.Find(id);
            if (tbTipoSangre == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSangre);
        }

        // POST: TipoSangre/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tps_Id,tps_nombre,tps_UsuarioCrea,tps_FechaCrea,tps_UsuarioModifica,tps_FechaModifica")] tbTipoSangre tbTipoSangre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoSangre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTipoSangre);
        }

        // GET: TipoSangre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSangre tbTipoSangre = db.tbTipoSangre.Find(id);
            if (tbTipoSangre == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSangre);
        }

        // POST: TipoSangre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoSangre tbTipoSangre = db.tbTipoSangre.Find(id);
            db.tbTipoSangre.Remove(tbTipoSangre);
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
