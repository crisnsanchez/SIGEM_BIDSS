

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    [SessionManager]
    public class AnticipoSalarioController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Funtion = new GeneralFunctions();


        // GET: AnticipoSalario
        public ActionResult Index()
        {
            try
            {
                var tbAnticipoSalario = db.tbAnticipoSalario.Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbEstado).Include(t => t.tbTipoSalario);
                return View(tbAnticipoSalario.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: AnticipoSalario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(tbAnticipoSalario, GeneralFunctions.Revisada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                }
            }
            if (tbAnticipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbAnticipoSalario);
        }


        // GET: AnticipoSalario/Details/5
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(tbAnticipoSalario, GeneralFunctions.Aprobada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                }
            }
            if (tbAnticipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbAnticipoSalario);
        }

        // GET: AnticipoSalario/Create
        public ActionResult Create()
        {
            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.emp_EsJefe == true select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();

            ViewBag.Ansal_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
            return View();
        }

        // POST: AnticipoSalario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ansal_Id,Ansal_Correlativo,emp_Id,Ansal_JefeInmediato,Ansal_GralFechaSolicitud,Ansal_MontoSolicitado,tpsal_id,Ansal_Justificacion,Ansal_Comentario,est_Id,Ansal_RazonRechazo,Ansal_UsuarioCrea,Ansal_FechaCrea")] tbAnticipoSalario tbAnticipoSalario)
        {
            string UserName = "",
                ErrorEmail = "";

            try
            {
                bool Result = false;
                int EmployeeID = Funtion.GetUser(out UserName);
                tbAnticipoSalario.emp_Id = EmployeeID;
                tbAnticipoSalario.Ansal_GralFechaSolicitud = Funtion.DatetimeNow();
                tbAnticipoSalario.est_Id = GeneralFunctions.Enviada;

                if (ModelState.IsValid)
                {
                    IEnumerable<object> Insert = null;
                    string ErrorMessage = "";

                    Insert = db.UDP_Adm_tbAnticipoSalario_Insert(EmployeeID,
                        tbAnticipoSalario.Ansal_JefeInmediato,
                        Funtion.DatetimeNow(),
                        tbAnticipoSalario.Ansal_MontoSolicitado,
                        tbAnticipoSalario.tpsal_id,
                        tbAnticipoSalario.Ansal_Justificacion,
                        tbAnticipoSalario.Ansal_Comentario,
                        tbAnticipoSalario.est_Id,
                        tbAnticipoSalario.Ansal_RazonRechazo,
                        EmployeeID,
                        Funtion.DatetimeNow());
                    foreach (UDP_Adm_tbAnticipoSalario_Insert_Result Res in Insert)
                        ErrorMessage = Res.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Funtion.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    }
                    else
                    {
                        Result = Funtion.LeerDatos(out ErrorEmail, ErrorMessage);
                        var EmpJefe = db.tbEmpleado.Where(x => x.emp_Id == tbAnticipoSalario.Ansal_JefeInmediato).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                        var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                        Funtion.LeerDatosSol(out string pvMensajeError, EmpJefe.emp_CorreoElectronico, EmpJefe.emp_Nombres, GetEmployee.emp_Nombres);

                        if (!Result) Funtion.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);
                        TempData["swalfunction"] = GeneralFunctions.sol_Enviada;
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                Funtion.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ex.Message.ToString());
            }
            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                            where _tbEmp.emp_EsJefe == true
                                            select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();
            ViewBag.Ansal_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres", tbAnticipoSalario.Ansal_JefeInmediato);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbAnticipoSalario.tpsal_id);
            return View(tbAnticipoSalario);

        }

        // GET: AnticipoSalario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoSalario.emp_Id);
            ViewBag.Ansal_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoSalario.Ansal_JefeInmediato);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbAnticipoSalario.est_Id);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbAnticipoSalario.tpsal_id);
            return View(tbAnticipoSalario);
        }

        // POST: AnticipoSalario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ansal_Id,Ansal_Correlativo,emp_Id,Ansal_JefeInmediato,Ansal_GralFechaSolicitud,Ansal_MontoSolicitado,tpsal_id,Ansal_Justificacion,Ansal_Comentario,est_Id,Ansal_RazonRechazo,Ansal_UsuarioCrea,Ansal_FechaCrea,Ansal_UsuarioModifica,Ansal_FechaModifica")] tbAnticipoSalario tbAnticipoSalario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAnticipoSalario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoSalario.emp_Id);
            ViewBag.Ansal_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoSalario.Ansal_JefeInmediato);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbAnticipoSalario.est_Id);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbAnticipoSalario.tpsal_id);
            return View(tbAnticipoSalario);
        }


        public bool UpdateState(tbAnticipoSalario tbAnticipoSalario, int State, string RazonInactivacion)
        {
            string UserName = "",
                ErrorEmail = "";
            try
            {
                bool Result = false;
                int EmployeeID = Funtion.GetUser(out UserName);
                tbAnticipoSalario.emp_Id = EmployeeID;
                tbAnticipoSalario.Ansal_GralFechaSolicitud = Funtion.DatetimeNow();
                tbAnticipoSalario.est_Id = State;
                tbAnticipoSalario.Ansal_RazonRechazo = RazonInactivacion;

                IEnumerable<object> Update = null;
                string ErrorMessage = "";

                Update = db.UDP_Adm_tbAnticipoSalario_Update(tbAnticipoSalario.Ansal_Id,
                    EmployeeID,
                    tbAnticipoSalario.Ansal_JefeInmediato,
                    Funtion.DatetimeNow(),
                    tbAnticipoSalario.Ansal_MontoSolicitado,
                    tbAnticipoSalario.tpsal_id,
                    tbAnticipoSalario.Ansal_Justificacion,
                    tbAnticipoSalario.Ansal_Comentario,
                    tbAnticipoSalario.est_Id,
                    tbAnticipoSalario.Ansal_RazonRechazo,
                    EmployeeID,
                    Funtion.DatetimeNow());
                foreach (UDP_Adm_tbAnticipoSalario_Update_Result Res in Update)
                    ErrorMessage = Res.MensajeError;
                if (ErrorMessage.StartsWith("-1"))
                {
                    Funtion.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorMessage);
                    ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    return false;
                }
                else
                {

                    var EmpJefe = db.tbEmpleado.Where(x => x.emp_Id == tbAnticipoSalario.Ansal_JefeInmediato).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                    var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                    Funtion.LeerDatosSol(out string pvMensajeError, EmpJefe.emp_CorreoElectronico, EmpJefe.emp_Nombres, GetEmployee.emp_Nombres);

                    Result = Funtion.LeerDatos(out ErrorEmail, ErrorMessage);
                    if (!Result) Funtion.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Funtion.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ex.Message.ToString());
                return false;
            }
        }

        [HttpPost]
        public JsonResult Reject(int id, string Ansal_RazonRechazo)
        {
            var list = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(tbAnticipoSalario, GeneralFunctions.Rechazada, Ansal_RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                }
            }
            if (tbAnticipoSalario == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
                //return HttpNotFound();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: AnticipoSalario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario == null)
            {
                return HttpNotFound();
            }
            return View(tbAnticipoSalario);
        }

        // POST: AnticipoSalario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            db.tbAnticipoSalario.Remove(tbAnticipoSalario);
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
