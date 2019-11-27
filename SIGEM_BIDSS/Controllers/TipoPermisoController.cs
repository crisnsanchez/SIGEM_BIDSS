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
    public class tbTipoPermisoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: tbTipoPermiso
        public ActionResult Index()
        {
            try
            {
                return View(db.tbTipoPermiso.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: tbTipoPermiso/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoPermiso tbTipoPermiso = db.tbTipoPermiso.Find(id);
                if (tbTipoPermiso == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoPermiso);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: tbTipoPermiso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbTipoPermiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tperm_Id,tperm_Descripcion,tperm_UsuarioCrea,tperm_FechaCrea,tperm_UsuarioModifica,tperm_FechaModifica")] tbTipoPermiso tbTipoPermiso)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoPermiso_Insert(tbTipoPermiso.tperm_Descripcion, 1);
                    foreach (UDP_Gral_tbTipoPermiso_Insert_Result Permiso in List)
                        Msj = Permiso.MensajeError;
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

            return View(tbTipoPermiso);


        }


        // GET: tbTipoPermiso/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoPermiso tbTipoPermiso = db.tbTipoPermiso.Find(id);
                if (tbTipoPermiso == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoPermiso);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: tbTipoPermiso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tperm_Id,tperm_Descripcion,tperm_UsuarioCrea,tperm_FechaCrea,tperm_UsuarioModifica,tperm_FechaModifica")] tbTipoPermiso tbTipoPermiso)
        {
            try
            {
                IEnumerable<Object> List = null;
                string Msj = "";
                List = db.UDP_Gral_tbTipoPermiso_Update(tbTipoPermiso.tperm_Id,tbTipoPermiso.tperm_Descripcion, 1);
                foreach (UDP_Gral_tbTipoPermiso_Update_Result Moneda in List)
                    Msj = Moneda.MensajeError;
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

            return View(tbTipoPermiso);


        }


        // GET: tbTipoPermiso/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbTipoPermiso tbTipoPermiso = db.tbTipoPermiso.Find(id);
                if (tbTipoPermiso == null)
                {
                    return HttpNotFound();
                }
                return View(tbTipoPermiso);
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: tbTipoPermiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbTipoPermiso tbTipoPermiso = db.tbTipoPermiso.Find(id);
            db.tbTipoPermiso.Remove(tbTipoPermiso);
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
