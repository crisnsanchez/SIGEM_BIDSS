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

        // GET Existencia del empleado y levantar modal
        [HttpPost]
        public JsonResult GetExistencia(int Sueldo)
        {

            var Existe = (from based in db.tbSueldo where based.emp_Id == Sueldo select new { empID = based.emp_Id }).FirstOrDefault(x => x.empID == 0);
            int Emplead_id = 0;
            if (Existe != null)
            {
                Emplead_id = Existe.empID;

            }
            return Json(Emplead_id, JsonRequestBehavior.AllowGet);
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

            int id = Convert.ToInt32(Session["EmpID"]);
            ViewBag.empIsNull = id;

            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.est_Id == 5 select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();



            var _tbEmpleado = (from _tdemp in db.tbEmpleado
                               where _tdemp.emp_Id == id
                               select new { empId = _tdemp.emp_Id, Nombre = _tdemp.emp_Nombres + " " + _tdemp.emp_Apellidos }).FirstOrDefault();
            if (_tbEmpleado != null)
            {

                tbSueldo tbSueldo = new tbSueldo
                {
                    emp_Id = _tbEmpleado.empId,
                    NombreEmpleado = _tbEmpleado.Nombre
                };

                ViewBag.emp_Id = new SelectList(Employee, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
                ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Nombre");
                return View(tbSueldo);
            }
            ViewBag.emp_Id = new SelectList(Employee, "emp_Id", "emp_Nombres");
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Nombre");
            return View();

        }


        // POST: Sueldo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sue_Id,emp_Id,sue_Cantidad,tmo_Id,Cantidad,Employee")] tbSueldo tbSueldo)
        {

            //decimal Cantidad = Convert.ToDecimal(tbSueldo.sue_Cantidad.ToString().Replace(",", ""));
            string UserName = "";
            try
            {
                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.est_Id == 5 select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();

                int EmployeeID = Function.GetUser(out UserName);

                if (ModelState.IsValid)
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    string _Cant = tbSueldo.Cantidad.Replace(",", "");
                    tbSueldo.sue_Cantidad = Convert.ToDecimal(_Cant);
                    _List = db.UDP_rrhh_tbSueldo_Insert(tbSueldo.emp_Id, tbSueldo.sue_Cantidad, tbSueldo.tmo_Id, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_rrhh_tbSueldo_Insert_Result Sueldo in _List)
                        MsjError = Sueldo.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("Sueldo", "CreatePost", UserName, MsjError);
                        ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Nombre", tbSueldo.tmo_Id);
                        ViewBag.emp_Id = new SelectList(Employee, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbSueldo);
                    }
                    if (MsjError.StartsWith("-2"))
                    {

                        ModelState.AddModelError("", "Ya existe un sueldo con el mismo nombre.");
                        ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Nombre", tbSueldo.tmo_Id);
                        ViewBag.emp_Id = new SelectList(Employee, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
                        return View(tbSueldo);
                    }
                    else
                    {
                        if (Session["EmpID"] == null)
                        {
                            TempData["swalfunction"] = GeneralFunctions._isCreated;
                            return RedirectToAction("Index");
                        }

                        else
                        {
                            Session["EmpID"] = null;
                            TempData["swalfunction"] = GeneralFunctions._isCreated;
                            return RedirectToAction("Index","Empleado");
                        }
                    }
                }
                ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Nombre", tbSueldo.tmo_Id);
                ViewBag.emp_Id = new SelectList(Employee, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
                return View(tbSueldo);
            }
            catch (Exception Ex)
            {
                Function.BitacoraErrores("Sueldo", "CreatePost", UserName, Ex.Message.ToString());
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Nombre", tbSueldo.tmo_Id);
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
            tbSueldo.Cantidad = Convert.ToString(tbSueldo.sue_Cantidad);
            if (tbSueldo == null)
            {
                return HttpNotFound();
            }
            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.est_Id == 5 select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();

            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Nombre", tbSueldo.tmo_Id);
            ViewBag.emp_Id = new SelectList(Employee, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
            return View(tbSueldo);



        }

        // POST: Sueldo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sue_Id,emp_Id,sue_Cantidad,tmo_Id,Cantidad,Employee")] tbSueldo tbSueldo)
        {
            string UserName = "";
            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.est_Id == 5 select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();

            try
            {

                int EmployeeID = Function.GetUser(out UserName);
                int Existe = (from based in db.tbSueldo where based.emp_Id == tbSueldo.emp_Id select based).Count();

                if (Existe > 0)
                {
                    ModelState.AddModelError("", "Este empleado ya posee un sueldo.");
                }
                if (ModelState.IsValid)
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    string _Cant = tbSueldo.Cantidad.Replace(",", "");
                    tbSueldo.sue_Cantidad = Convert.ToDecimal(_Cant);
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
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Nombre", tbSueldo.tmo_Id);
            ViewBag.emp_Id = new SelectList(Employee, "emp_Id", "emp_Nombres", tbSueldo.emp_Id);
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
