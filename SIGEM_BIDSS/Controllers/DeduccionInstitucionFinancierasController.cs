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
    public class DeduccionInstitucionFinancierasController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: DeduccionInstitucionFinancieras
        public ActionResult Index()
        {
            var tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Include(t => t.tbEmpleado).Include(t => t.tbInstitucionFinanciera);
            return View(tbDeduccionInstitucionFinanciera.ToList());
        }

        // GET: DeduccionInstitucionFinancieras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(id);
            if (tbDeduccionInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbDeduccionInstitucionFinanciera);
        }

        // GET: DeduccionInstitucionFinancieras/Create
        public ActionResult Create()
        {
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.insf_IdInstitucionFinanciera = new SelectList(db.tbInstitucionFinanciera, "insf_IdInstitucionFinanciera", "insf_DescInstitucionFinanc");
            return View();
        }

        // POST: DeduccionInstitucionFinancieras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "deif_IdDeduccionInstFinanciera,emp_Id,insf_IdInstitucionFinanciera,deif_Monto,deif_Comentarios,deif_UsuarioCrea,deif_FechaCrea,deif_UsuarioModifica,deif_FechaModifica,deif_Activo,deif_Pagado")] tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera)
        {
            if (ModelState.IsValid)
            {
                db.tbDeduccionInstitucionFinanciera.Add(tbDeduccionInstitucionFinanciera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbDeduccionInstitucionFinanciera.emp_Id);
            ViewBag.insf_IdInstitucionFinanciera = new SelectList(db.tbInstitucionFinanciera, "insf_IdInstitucionFinanciera", "insf_DescInstitucionFinanc", tbDeduccionInstitucionFinanciera.insf_IdInstitucionFinanciera);
            return View(tbDeduccionInstitucionFinanciera);
        }

        // GET: DeduccionInstitucionFinancieras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(id);
            if (tbDeduccionInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbDeduccionInstitucionFinanciera.emp_Id);
            ViewBag.insf_IdInstitucionFinanciera = new SelectList(db.tbInstitucionFinanciera, "insf_IdInstitucionFinanciera", "insf_DescInstitucionFinanc", tbDeduccionInstitucionFinanciera.insf_IdInstitucionFinanciera);
            return View(tbDeduccionInstitucionFinanciera);
        }

        // POST: DeduccionInstitucionFinancieras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "deif_IdDeduccionInstFinanciera,emp_Id,insf_IdInstitucionFinanciera,deif_Monto,deif_Comentarios,deif_UsuarioCrea,deif_FechaCrea,deif_UsuarioModifica,deif_FechaModifica,deif_Activo,deif_Pagado")] tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbDeduccionInstitucionFinanciera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbDeduccionInstitucionFinanciera.emp_Id);
            ViewBag.insf_IdInstitucionFinanciera = new SelectList(db.tbInstitucionFinanciera, "insf_IdInstitucionFinanciera", "insf_DescInstitucionFinanc", tbDeduccionInstitucionFinanciera.insf_IdInstitucionFinanciera);
            return View(tbDeduccionInstitucionFinanciera);
        }

        // GET: DeduccionInstitucionFinancieras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(id);
            if (tbDeduccionInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbDeduccionInstitucionFinanciera);
        }

        // POST: DeduccionInstitucionFinancieras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDeduccionInstitucionFinanciera tbDeduccionInstitucionFinanciera = db.tbDeduccionInstitucionFinanciera.Find(id);
            db.tbDeduccionInstitucionFinanciera.Remove(tbDeduccionInstitucionFinanciera);
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
