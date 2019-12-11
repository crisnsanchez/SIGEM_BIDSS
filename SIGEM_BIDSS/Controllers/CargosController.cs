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
    public class CargosController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        Models.Helpers Function = new Models.Helpers();

        // GET: Cargos
        public ActionResult Index()
        {
            try
            {
                var tbPuesto = db.tbPuesto.Include(t => t.tbArea);
                return View(tbPuesto.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }


        // GET: Cargos/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbPuesto tbPuesto = db.tbPuesto.Find(id);
                if (tbPuesto == null)
                {
                    return HttpNotFound();
                }
                return View(tbPuesto);
            }

            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }
            // GET: Cargos/Create
            public ActionResult Create()
        {
            try
            {
                ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
                return View();
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Cargos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pto_Id,are_Id,pto_Descripcion,pto_UsuarioCrea,pto_FechaCrea,pto_UsuarioModifica,pto_FechaModifica")] tbPuesto tbPuesto)
        {
            ViewBag.are_Id = new SelectList(db.tbPuesto, "are_Id", "are_Descripcion", tbPuesto.are_Id);

            if (ModelState.IsValid)

            {
                ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbPuesto_Insert(tbPuesto.are_Id, tbPuesto.pto_Descripcion, 1, Function.DatetimeNow());
                    foreach (UDP_Gral_tbPuesto_Insert_Result Puesto in List)
                        Msj = Puesto.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    if (Msj.StartsWith("-2"))
                    {
                        ModelState.AddModelError("", "Esta área ya posee un cargo igual.");

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

            }

            return View(tbPuesto);
        }


        // GET: Cargos/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbPuesto tbPuesto = db.tbPuesto.Find(id);
                if (tbPuesto == null)
                {
                    return HttpNotFound();
                }
                ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbPuesto.are_Id);
                return View(tbPuesto);
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Cargos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pto_Id,are_Id,pto_Descripcion,pto_UsuarioCrea,pto_FechaCrea,pto_UsuarioModifica,pto_FechaModifica")] tbPuesto tbPuesto)
        {
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbPuesto.are_Id);


            if (ModelState.IsValid)
                if (db.tbPuesto.Any(a => a.pto_Descripcion == tbPuesto.pto_Descripcion && a.are_Id == tbPuesto.are_Id))
                {
                    ModelState.AddModelError("", "Esta área ya posee un cargo igual.");
                    return View(tbPuesto);
                }

            try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbPuesto_Update(tbPuesto.pto_Id, tbPuesto.are_Id, tbPuesto.pto_Descripcion, 1);
                    foreach (UDP_Gral_tbPuesto_Update_Result TipoSangre in List)
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


            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            return View(tbPuesto);
        }

        // GET: Cargos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuesto tbPuesto = db.tbPuesto.Find(id);
            if (tbPuesto == null)
            {
                return HttpNotFound();
            }
            return View(tbPuesto);
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPuesto tbPuesto = db.tbPuesto.Find(id);
            db.tbPuesto.Remove(tbPuesto);
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
