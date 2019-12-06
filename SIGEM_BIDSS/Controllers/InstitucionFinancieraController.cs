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
    public class InstitucionFinancieraController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        Helpers Function = new Helpers();
        // GET: InstitucionFinanciera
        public ActionResult Index()
        {
            return View(db.tbInstitucionFinanciera.ToList());
        }

        // GET: InstitucionFinanciera/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionFinanciera tbInstitucionFinanciera = db.tbInstitucionFinanciera.Find(id);
            if (tbInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionFinanciera);
        }

        // GET: InstitucionFinanciera/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstitucionFinanciera/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "insf_Id,insf_Nombre,insf_Contacto,insf_Telefono,insf_Correo,insf_Activo,insf_UsuarioCrea,insf_FechaCrea,insf_UsuarioModifica,insf_FechaModifica")] tbInstitucionFinanciera tbInstitucionFinanciera)
        {
            try
            {
                IEnumerable<Object> List = null;
                string Msj = "";
                List = db.UDP_Plani_tbInstitucionFinanciera_Insert(  tbInstitucionFinanciera.insf_Id,
                                                                     tbInstitucionFinanciera.insf_Nombre,
                                                                     tbInstitucionFinanciera.insf_Contacto,
                                                                     tbInstitucionFinanciera.insf_Telefono,
                                                                     tbInstitucionFinanciera.insf_Correo,
                                                                     1,
                                                                     Function.DatetimeNow(),
                                                                     tbInstitucionFinanciera.insf_Activo);
                foreach (UDP_Plani_tbInstitucionFinanciera_Insert_Result TipoSangre in List)
                    Msj = TipoSangre.MensajeError;
                if (Msj.StartsWith("-1"))
                {
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {

                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View();
            }



            return View();
        }

        // GET: InstitucionFinanciera/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionFinanciera tbInstitucionFinanciera = db.tbInstitucionFinanciera.Find(id);
            if (tbInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionFinanciera);
        }

        // POST: InstitucionFinanciera/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "insf_Id,insf_Nombre,insf_Contacto,insf_Telefono,insf_Correo,insf_Activo,insf_UsuarioCrea,insf_FechaCrea,insf_UsuarioModifica,insf_FechaModifica")] tbInstitucionFinanciera tbInstitucionFinanciera)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Plani_tbInstitucionFinanciera_Update(tbInstitucionFinanciera.insf_Id, tbInstitucionFinanciera.insf_Nombre,
                                                                         tbInstitucionFinanciera.insf_Contacto,
                                                                         tbInstitucionFinanciera.insf_Telefono,
                                                                         tbInstitucionFinanciera.insf_Correo, 1,
                                                                         Function.DatetimeNow(),
                                                                         tbInstitucionFinanciera.insf_Activo);
                    foreach (UDP_Plani_tbInstitucionFinanciera_Update_Result TipoSangre in List)
                        Msj = TipoSangre.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View();
                }



            return View(tbInstitucionFinanciera);
        }

        // GET: InstitucionFinanciera/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInstitucionFinanciera tbInstitucionFinanciera = db.tbInstitucionFinanciera.Find(id);
            if (tbInstitucionFinanciera == null)
            {
                return HttpNotFound();
            }
            return View(tbInstitucionFinanciera);
        }

        // POST: InstitucionFinanciera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInstitucionFinanciera tbInstitucionFinanciera = db.tbInstitucionFinanciera.Find(id);
            db.tbInstitucionFinanciera.Remove(tbInstitucionFinanciera);
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
