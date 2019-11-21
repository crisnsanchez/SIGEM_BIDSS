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
    public class TipoSalarioController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: TipoSalario
        public ActionResult Index()
        {
            return View(db.tbTipoSalario.ToList());
        }

        // GET: TipoSalario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalario tbTipoSalario = db.tbTipoSalario.Find(id);
            if (tbTipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalario);
        }

        // GET: TipoSalario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoSalario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tpsal_id,tpsal_Descripcion,tpsal_UsuarioCrea,tpsal_FechaCrea,tpsal_UsuarioModifica,tpsal_FechaModifica")] tbTipoSalario tbTipoSalario)
        {
            if (ModelState.IsValid)
            {
                db.tbTipoSalario.Add(tbTipoSalario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbTipoSalario);
        }

        // GET: TipoSalario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalario tbTipoSalario = db.tbTipoSalario.Find(id);
            if (tbTipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalario);
        }

        // POST: TipoSalario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tpsal_id,tpsal_Descripcion,tpsal_UsuarioCrea,tpsal_FechaCrea,tpsal_UsuarioModifica,tpsal_FechaModifica")] tbTipoSalario tbTipoSalario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbTipoSalario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbTipoSalario);
        }

        // GET: TipoSalario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoSalario tbTipoSalario = db.tbTipoSalario.Find(id);
            if (tbTipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoSalario);
        }

        // POST: TipoSalario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoSalario tbTipoSalario = db.tbTipoSalario.Find(id);
            db.tbTipoSalario.Remove(tbTipoSalario);
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
