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
    public class TipoMonedaController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: TipoMoneda
        public ActionResult Index()
        {
            return View(db.tbTipoMoneda.ToList());
        }

        // GET: TipoMoneda/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMoneda tbTipoMoneda = db.tbTipoMoneda.Find(id);
            if (tbTipoMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoMoneda);
        }

        // GET: TipoMoneda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMoneda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tmo_Id,tmo_Abreviatura,tmo_Nombre,tmo_UsuarioCrea,tmo_FechaCrea,tmo_UsuarioModifica,tmo_FechaModifica")] tbTipoMoneda tbTipoMoneda)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoMoneda_Insert(tbTipoMoneda.tmo_Abreviatura, tbTipoMoneda.tmo_Nombre, tbTipoMoneda.tmo_UsuarioCrea=1);
                    foreach (UDP_Gral_tbTipoMoneda_Insert_Result TipoMoneda in List)
                        Msj = TipoMoneda.MensajeError;
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
            return View(tbTipoMoneda);
        }
        
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMoneda tbTipoMoneda = db.tbTipoMoneda.Find(id);
            if (tbTipoMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoMoneda);
        }

        // POST: TipoMoneda/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tmo_Id,tmo_Abreviatura,tmo_Nombre,tmo_UsuarioCrea,tmo_FechaCrea,tmo_UsuarioModifica,tmo_FechaModifica")] tbTipoMoneda tbTipoMoneda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoMoneda_Update(tbTipoMoneda.tmo_Id, tbTipoMoneda.tmo_Abreviatura, tbTipoMoneda.tmo_Nombre,tbTipoMoneda.tmo_UsuarioModifica = 1);
                    foreach (UDP_Gral_tbTipoMoneda_Update_Result TipoSangre in List)
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
            }
            return View(tbTipoMoneda);
        
    }

        // GET: TipoMoneda/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbTipoMoneda tbTipoMoneda = db.tbTipoMoneda.Find(id);
            if (tbTipoMoneda == null)
            {
                return HttpNotFound();
            }
            return View(tbTipoMoneda);
        }

        // POST: TipoMoneda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbTipoMoneda tbTipoMoneda = db.tbTipoMoneda.Find(id);
            db.tbTipoMoneda.Remove(tbTipoMoneda);
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
