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
            try
            {
                return View(db.tbMoneda.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Moneda/Details/5
        public ActionResult Details(short? id)
        {
            try
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
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
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
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    //List = db.UDP_Gral_tbMoneda_Insert(tbMoneda.tmo_Id,tbMoneda.tmo_Abreviatura,tbMoneda.tmo_Nombre, 1);
                    foreach (UDP_Gral_tbMoneda_Insert_Result Moneda in List)
                        Msj = Moneda.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {

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

            return View(tbMoneda);


        }


        // GET: Moneda/Edit/5
        public ActionResult Edit(short? id)
        {
            try
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
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");

            }
        }
        // POST: Moneda/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tmo_Id,tmo_Abreviatura,tmo_Nombre,tmo_UsuarioCrea,tmo_FechaCrea,tmo_UsuarioModifica,tmo_FechaModifica")] tbMoneda tbMoneda)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    //List = db.UDP_Gral_tbMoneda_Update(tbMoneda.tmo_Id, tbMoneda.tmo_Abreviatura,tbMoneda.tmo_Nombre, 1);
                    foreach (UDP_Gral_tbMoneda_Update_Result Moneda in List)
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
