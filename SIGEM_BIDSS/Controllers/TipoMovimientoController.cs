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
    public class TipoMovimientoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: TipoMovimiento
        public ActionResult Index()
        {
            try
            {
                return View(db.tbTipoMovimiento.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: TipoMovimiento/Details/5
        public ActionResult Details(int? id)
        {
            try
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
            catch(Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            }
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
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_TipoMovimiento_Insert(tbTipoMovimiento.tipmo_Movimiento, 1);
                    foreach (UDP_Gral_TipoMovimiento_Insert_Result tbMovimiento in List)
                        Msj = tbMovimiento.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    else
                    {
                        TempData["swalfunction"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View();
                }

            return View(tbTipoMovimiento);



        }

        // GET: TipoMovimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");

            }
        }
        // POST: TipoMovimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tipmo_id,tipmo_Movimiento,tipmo_UsuarioCrea,tipmo_FechaCrea,tipmo_UsuarioModifica,tipmo_FechaModifica")] tbTipoMovimiento tbTipoMovimiento)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbTipoMovimiento_Update(tbTipoMovimiento.tipmo_id, tbTipoMovimiento.tipmo_Movimiento, 1);
                    foreach (UDP_Gral_tbTipoMovimiento_Update_Result Movimiento in List)
                        Msj = Movimiento.MensajeError;
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
