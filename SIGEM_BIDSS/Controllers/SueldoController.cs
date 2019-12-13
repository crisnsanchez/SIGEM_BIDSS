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
    public class SueldoController : BaseController
    {

        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        Models.Helpers Function = new Models.Helpers();
        // GET: Sueldo
        public ActionResult Index()
        {
            var tbSueldo = db.tbSueldo.Include(t => t.tbMoneda).Include(t => t.tbEmpleado);
            return View(tbSueldo.ToList());
        }

        // GET: Sueldo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSueldo tbSueldo = db.tbSueldo.Find(id);
            if (tbSueldo == null)
            {
                return HttpNotFound();
            }
            return View(tbSueldo);
        }

        // GET: Sueldo/Create
        public ActionResult Create(int? id)
        {
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            //tbSueldo.sue_Id = tbEmpleado.emp_Id;
            //tbSueldo.NombreEmpleado = tbEmpleado.emp_Nombres +" "+ tbEmpleado.emp_Apellidos;
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            return View();
        }

        // POST: Sueldo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sue_Id,emp_Id,sue_Cantidad,tmo_Id,sue_UsuarioCrea,sue_FechaCrea")] tbSueldo tbSueldo)
        {
            try
            {
                IEnumerable<object> _List = null;
                string MsjError = "";
                _List = db.UDP_rrhh_tbSueldo_Insert(tbSueldo.emp_Id,tbSueldo.sue_Cantidad,tbSueldo.tmo_Id, 1, Function.DatetimeNow());
                foreach (UDP_rrhh_tbSueldo_Insert_Result Sueldo in _List)
                    MsjError = Sueldo.MensajeError;
                if (MsjError.StartsWith("-1"))
                {
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbSueldo);
                }
                if (MsjError.StartsWith("-2"))
                {

                    ModelState.AddModelError("", "Ya existe un sueldo con el mismo nombre.");
                    return View(tbSueldo);
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
                ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura", tbSueldo.tmo_Id);
                ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
                return View(tbSueldo);
            }



        }

        // GET: Sueldo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSueldo tbSueldo = db.tbSueldo.Find(id);
            if (tbSueldo == null)
            {
                return HttpNotFound();
            }
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura", tbSueldo.tmo_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
            return View(tbSueldo);
        }

        // POST: Sueldo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sue_Id,emp_Id,sue_Cantidad,tmo_Id,sue_UsuarioModifica,sue_FechaModifica")] tbSueldo tbSueldo)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    _List = db.UDP_rrhh_tbSueldo_Update(tbSueldo.sue_Id, tbSueldo.emp_Id, tbSueldo.sue_Cantidad, tbSueldo.tmo_Id, 1, Function.DatetimeNow());
                    foreach (UDP_rrhh_tbSueldo_Update_Result _sueldo in _List)
                        MsjError = _sueldo.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        return View(tbSueldo);
                    }
                    if (MsjError.StartsWith("-2"))
                    {

                        ModelState.AddModelError("", "Ya existe un sueldo con el mismo nombre.");
                        return View(tbSueldo);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbSueldo);
                }
            }
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura", tbSueldo.tmo_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
            return View(tbSueldo);
        }

        // GET: Sueldo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSueldo tbSueldo = db.tbSueldo.Find(id);
            if (tbSueldo == null)
            {
                return HttpNotFound();
            }
            return View(tbSueldo);
        }

        // POST: Sueldo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSueldo tbSueldo = db.tbSueldo.Find(id);
            db.tbSueldo.Remove(tbSueldo);
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
