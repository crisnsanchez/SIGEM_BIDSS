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
    [Authorize]
    public class TipoSolicitudController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: TipoSolicitud
        public ActionResult Index()
        {
            try
            {
                return View(db.tbTipoSolicitud.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: TipoSolicitud/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoSolicitud tbTipoSolicitud = db.tbTipoSolicitud.Find(id);
                if (tbTipoSolicitud == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoSolicitud);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: TipoSolicitud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoSolicitud/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipsol_Id,tipsol_Descripcion,tipsol_UsuarioCrea,tipsol_FechaCrea,tipsol_UsuarioModifica,tipsol_FechaModifica")] tbTipoSolicitud tbTipoSolicitud)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoSolicitud_Insert(tbTipoSolicitud.tipsol_Descripcion, 1);
                    foreach (UDP_Gral_tbTipoSolicitud_Insert_Result Tiposolicitud in List)
                        Msj = Tiposolicitud.MensajeError;
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

            return View(tbTipoSolicitud);






        }
        // GET: TipoSolicitud/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoSolicitud tbTipoSolicitud = db.tbTipoSolicitud.Find(id);
                if (tbTipoSolicitud == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoSolicitud);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: TipoSolicitud/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tipsol_Id,tipsol_Descripcion,tipsol_UsuarioCrea,tipsol_FechaCrea,tipsol_UsuarioModifica,tipsol_FechaModifica")] tbTipoSolicitud tbTipoSolicitud)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoSolicitud_Update(tbTipoSolicitud.tipsol_Id, tbTipoSolicitud.tipsol_Descripcion, 1);
                    foreach (UDP_Gral_tbTipoSolicitud_Update_Result TipoSangre in List)
                        Msj = TipoSangre.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
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

            return View(tbTipoSolicitud);


        }
      
        

        // GET: TipoSolicitud/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoSolicitud tbTipoSolicitud = db.tbTipoSolicitud.Find(id);
                if (tbTipoSolicitud == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoSolicitud);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: TipoSolicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoSolicitud tbTipoSolicitud = db.tbTipoSolicitud.Find(id);
            db.tbTipoSolicitud.Remove(tbTipoSolicitud);
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
