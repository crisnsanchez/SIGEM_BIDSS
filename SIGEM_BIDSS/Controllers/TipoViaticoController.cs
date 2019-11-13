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
    public class TipoViaticoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: TipoViatico
        public ActionResult Index()
        {
            return View(db.tbTipoViatico.ToList());
        }

        // GET: TipoViatico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoViatico tbTipoViatico = db.tbTipoViatico.Find(id);
            if (tbTipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoViatico);
        }

        // GET: TipoViatico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoViatico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tpv_Id,tpv_Descripcion,tpv_UsuarioCrea,tpv_FechaCrea,tpv_UsuarioModifica,tpv_FechaModifica")] tbTipoViatico tbTipoViatico)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoViatico.Add(tbTipoViatico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTipoViatico);
        }

        // GET: TipoViatico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoViatico tbTipoViatico = db.tbTipoViatico.Find(id);
            if (tbTipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoViatico);
        }

        // POST: TipoViatico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tpv_Id,tpv_Descripcion,tpv_UsuarioCrea,tpv_FechaCrea,tpv_UsuarioModifica,tpv_FechaModifica")] tbTipoViatico tbTipoViatico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoViatico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTipoViatico);
        }

        // GET: TipoViatico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoViatico tbTipoViatico = db.tbTipoViatico.Find(id);
            if (tbTipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoViatico);
        }

        // POST: TipoViatico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoViatico tbTipoViatico = db.tbTipoViatico.Find(id);
            db.tbTipoViatico.Remove(tbTipoViatico);
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
