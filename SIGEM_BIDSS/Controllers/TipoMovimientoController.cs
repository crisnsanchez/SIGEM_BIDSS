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
    public class TipoMovimientoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: TipoMovimiento
        public ActionResult Index()
        {
            return View(db.tbTipoMovimiento.ToList());
        }

        // GET: TipoMovimiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMovimiento tbTipoMovimiento = db.tbTipoMovimiento.Find(id);
            if (tbTipoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoMovimiento);
        }

        // GET: TipoMovimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMovimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipmo_id,tipmo_Movimiento,tipmo_UsuarioCrea,tipmo_FechaCrea,tipmo_UsuarioModifica,tipmo_FechaModifica")] tbTipoMovimiento tbTipoMovimiento)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoMovimiento.Add(tbTipoMovimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTipoMovimiento);
        }

        // GET: TipoMovimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMovimiento tbTipoMovimiento = db.tbTipoMovimiento.Find(id);
            if (tbTipoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoMovimiento);
        }

        // POST: TipoMovimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tipmo_id,tipmo_Movimiento,tipmo_UsuarioCrea,tipmo_FechaCrea,tipmo_UsuarioModifica,tipmo_FechaModifica")] tbTipoMovimiento tbTipoMovimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoMovimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTipoMovimiento);
        }

        // GET: TipoMovimiento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMovimiento tbTipoMovimiento = db.tbTipoMovimiento.Find(id);
            if (tbTipoMovimiento == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoMovimiento);
        }

        // POST: TipoMovimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoMovimiento tbTipoMovimiento = db.tbTipoMovimiento.Find(id);
            db.tbTipoMovimiento.Remove(tbTipoMovimiento);
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
