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
        GeneralFunctions Function = new GeneralFunctions();

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
        public ActionResult Create()
        {

            //int id = Convert.ToInt32(Session["EmpID"]);
            //var _tbEmpleado = (from _tdemp in db.tbEmpleado 
            //                   where _tdemp.emp_Id == id 
            //                   select new {empId = _tdemp.emp_Id, Nombre = _tdemp.emp_Nombres + " " + _tdemp.emp_Apellidos }).FirstOrDefault();
            //tbSueldo tbSueldo = new tbSueldo
            //{
            //    emp_Id = _tbEmpleado.empId,
            //    NombreEmpleado = _tbEmpleado.Nombre
            //};
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura");
            return View();
        }

        // POST: Sueldo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sue_Id,emp_Id,sue_Cantidad,tmo_Id")] tbSueldo tbSueldo)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    _List = db.UDP_rrhh_tbSueldo_Insert(tbSueldo.emp_Id, tbSueldo.sue_Cantidad, tbSueldo.tmo_Id, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_rrhh_tbSueldo_Insert_Result Sueldo in _List)
                        MsjError = Sueldo.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Sueldo", "CreatePost", UserName, MsjError);
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
                        Session["EmpID"] = null;
                        TempData["swalfunction"] = GeneralFunctions._isCreated;
                        return RedirectToAction("Index");
                    }
                }
                return View(tbSueldo);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Sueldo", "CreatePost", UserName, Ex.Message.ToString());
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
        public ActionResult Edit([Bind(Include = "sue_Id,emp_Id,sue_Cantidad,tmo_Id")] tbSueldo tbSueldo)
        {
            string UserName = "";
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                if (ModelState.IsValid)
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    _List = db.UDP_rrhh_tbSueldo_Update(tbSueldo.sue_Id, tbSueldo.emp_Id, tbSueldo.sue_Cantidad, tbSueldo.tmo_Id, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_rrhh_tbSueldo_Update_Result _sueldo in _List)
                        MsjError = _sueldo.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Sueldo", "EditPost", UserName, MsjError);
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
                        TempData["swalfunction"] = GeneralFunctions._isEdited;
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Sueldo", "EditPost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbSueldo);
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
