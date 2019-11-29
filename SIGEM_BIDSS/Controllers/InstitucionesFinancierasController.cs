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
    public class InstitucionesFinancierasController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: InstitucionesFinancieras
        public ActionResult Index()
        {
            return View(db.tbInstitucionesFinancieras.ToList());
        }

        // GET: InstitucionesFinancieras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionesFinancieras tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Find(id);
            if (tbInstitucionesFinancieras == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionesFinancieras);
        }

        // GET: InstitucionesFinancieras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstitucionesFinancieras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "insf_IdInstitucionFinanciera,insf_DescInstitucionFinanc,insf_Contacto,insf_Telefono,insf_Correo,insf_UsuarioCrea,insf_FechaCrea,insf_UsuarioModifica,insf_FechaModifica,insf_Activo")] tbInstitucionesFinancieras tbInstitucionesFinancieras)
        {
            if (ModelState.IsValid)
            {
                db.tbInstitucionesFinancieras.Add(tbInstitucionesFinancieras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbInstitucionesFinancieras);
        }

        // GET: InstitucionesFinancieras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionesFinancieras tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Find(id);
            if (tbInstitucionesFinancieras == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionesFinancieras);
        }

        // POST: InstitucionesFinancieras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "insf_IdInstitucionFinanciera,insf_DescInstitucionFinanc,insf_Contacto,insf_Telefono,insf_Correo,insf_UsuarioCrea,insf_FechaCrea,insf_UsuarioModifica,insf_FechaModifica,insf_Activo")] tbInstitucionesFinancieras tbInstitucionesFinancieras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbInstitucionesFinancieras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbInstitucionesFinancieras);
        }

        // GET: InstitucionesFinancieras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionesFinancieras tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Find(id);
            if (tbInstitucionesFinancieras == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionesFinancieras);
        }

        // POST: InstitucionesFinancieras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInstitucionesFinancieras tbInstitucionesFinancieras = db.tbInstitucionesFinancieras.Find(id);
            db.tbInstitucionesFinancieras.Remove(tbInstitucionesFinancieras);
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
