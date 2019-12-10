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
    public class UnidadMedidaController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        private GeneralFunctions GFC = new GeneralFunctions();
        // GET: UnidadMedida
        public ActionResult Index()
        {
            return View(db.tbUnidadMedida.ToList());
        }

        // GET: UnidadMedida/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidadMedida);
        }

        // GET: UnidadMedida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnidadMedida/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "uni_Id,uni_Descripcion,uni_Abreviatura,uni_UsuarioCrea,uni_FechaCrea,uni_UsuarioModifica,uni_FechaModifica")] tbUnidadMedida tbUnidadMedida)
        {
            if (ModelState.IsValid)
            {
                db.tbUnidadMedida.Add(tbUnidadMedida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbUnidadMedida);
        }

        // GET: UnidadMedida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidadMedida);
        }

        // POST: UnidadMedida/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "uni_Id,uni_Descripcion,uni_Abreviatura,uni_UsuarioCrea,uni_FechaCrea,uni_UsuarioModifica,uni_FechaModifica")] tbUnidadMedida tbUnidadMedida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUnidadMedida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbUnidadMedida);
        }

        // GET: UnidadMedida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            if (tbUnidadMedida == null)
            {
                return HttpNotFound();
            }
            return View(tbUnidadMedida);
        }

        // POST: UnidadMedida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUnidadMedida tbUnidadMedida = db.tbUnidadMedida.Find(id);
            db.tbUnidadMedida.Remove(tbUnidadMedida);
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
