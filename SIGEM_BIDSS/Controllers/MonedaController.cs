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
    public class MonedaController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Moneda
        public ActionResult Index()
        {
            return View(db.tbMoneda.ToList());
        }

        // GET: Moneda/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // GET: Moneda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Moneda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tmo_Id,tmo_Abreviatura,tmo_Nombre,tmo_UsuarioCrea,tmo_FechaCrea,tmo_UsuarioModifica,tmo_FechaModifica")] tbMoneda tbMoneda)
        {
            if (ModelState.IsValid)
            {
                db.tbMoneda.Add(tbMoneda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbMoneda);
        }

        // GET: Moneda/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // POST: Moneda/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tmo_Id,tmo_Abreviatura,tmo_Nombre,tmo_UsuarioCrea,tmo_FechaCrea,tmo_UsuarioModifica,tmo_FechaModifica")] tbMoneda tbMoneda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbMoneda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbMoneda);
        }

        // GET: Moneda/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            if (tbMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbMoneda);
        }

        // POST: Moneda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbMoneda tbMoneda = db.tbMoneda.Find(id);
            db.tbMoneda.Remove(tbMoneda);
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
