

using SIGEM_BIDSS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    [SessionManager]
    public class AnticipoSalarioController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();


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


        // GET: AnticipoSalario/Revisar
        [HttpPost]
        public JsonResult Revisar(int id, string Ansal_RazonRechazo)
        {
            var list = "";
            string IsFor =  "false";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(tbAnticipoSalario, GeneralFunctions.Revisada, Ansal_RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                    IsFor = "true";
                }
            }
            if (tbAnticipoSalario == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(IsFor, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create([Bind(Include = "Ansal_Id,Ansal_Correlativo,emp_Id,Ansal_JefeInmediato,Ansal_GralFechaSolicitud,Ansal_MontoSolicitado,tpsal_id,Ansal_Justificacion,Ansal_Comentario,est_Id,Ansal_RazonRechazo")] tbAnticipoSalario tbAnticipoSalario)
        {
            string UserName = "",
                ErrorEmail = "";
            try
            {
                bool Result = false, ResultAdm = false;
                int EmployeeID = Function.GetUser(out UserName);
                tbAnticipoSalario.emp_Id = EmployeeID;
                tbAnticipoSalario.Ansal_GralFechaSolicitud = Function.DatetimeNow();
                tbAnticipoSalario.est_Id = GeneralFunctions.Enviada;

                if (ModelState.IsValid)
                {
                    IEnumerable<object> Insert = null;
                    string ErrorMessage = "";

                    Insert = db.UDP_Adm_tbAnticipoSalario_Insert(EmployeeID,
                        tbAnticipoSalario.Ansal_JefeInmediato,
                        Function.DatetimeNow(),
                        tbAnticipoSalario.Ansal_MontoSolicitado,
                        tbAnticipoSalario.tpsal_id,
                        tbAnticipoSalario.Ansal_Justificacion,
                        tbAnticipoSalario.Ansal_Comentario,
                        tbAnticipoSalario.est_Id,
                        tbAnticipoSalario.Ansal_RazonRechazo,
                        EmployeeID,
                        Function.DatetimeNow());
                    foreach (UDP_Adm_tbAnticipoSalario_Insert_Result Res in Insert)
                        ErrorMessage = Res.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    }
                    else
                    {

                        var EmpJefe = db.tbEmpleado.Where(x => x.emp_Id == tbAnticipoSalario.Ansal_JefeInmediato).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                        var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                        var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, "", GetEmployee.emp_CorreoElectronico, GeneralFunctions.msj_Enviada, "");
                        ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GetEmployee.emp_Nombres, _Parameters.par_CorreoEmpresa, GeneralFunctions.msj_ToAdmin, "");

                        if (!Result) Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);

                        TempData["swalfunction"] = GeneralFunctions.sol_Enviada;
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                Function.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ex.Message.ToString());
            }
            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                            where _tbEmp.emp_EsJefe == true
                                            select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();
            ViewBag.Ansal_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres", tbAnticipoSalario.Ansal_JefeInmediato);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbAnticipoSalario.tpsal_id);
            return View(tbAnticipoSalario);

        }


       

        public bool UpdateState(tbAnticipoSalario tbAnticipoSalario, int State, string Ansal_RazonRechazo)
        {
            string UserName = "",
                ErrorEmail = "";
            try
            {
                bool Result = false;
                int EmployeeID = Function.GetUser(out UserName);
                tbAnticipoSalario.emp_Id = EmployeeID;
                tbAnticipoSalario.est_Id = State;
                tbAnticipoSalario.Ansal_RazonRechazo = Ansal_RazonRechazo;

                IEnumerable<object> Update = null;
                string ErrorMessage = "";

                Update = db.UDP_Adm_tbAnticipoSalario_Update(tbAnticipoSalario.Ansal_Id,
                                                            EmployeeID,
                                                            tbAnticipoSalario.Ansal_JefeInmediato,
                                                            tbAnticipoSalario.Ansal_GralFechaSolicitud,
                                                            tbAnticipoSalario.Ansal_MontoSolicitado,
                                                            tbAnticipoSalario.tpsal_id,
                                                            tbAnticipoSalario.Ansal_Justificacion,
                                                            tbAnticipoSalario.Ansal_Comentario,
                                                            tbAnticipoSalario.est_Id,
                                                            tbAnticipoSalario.Ansal_RazonRechazo,
                                                            EmployeeID,
                                                            Function.DatetimeNow());
                foreach (UDP_Adm_tbAnticipoSalario_Update_Result Res in Update)
                    ErrorMessage = Res.MensajeError;
                if (ErrorMessage.StartsWith("-1"))
                {
                    Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorMessage);
                    ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    return false;
                }
                else
                {
                    var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                    string _msj = "";
                    var reject = "";
                    switch (State)
                    {
                        case GeneralFunctions.Revisada:
                            _msj = GeneralFunctions.msj_Revisada;
                            break;
                        case GeneralFunctions.Aprobada:
                            _msj = GeneralFunctions.msj_Aprobada;
                            break;
                        case GeneralFunctions.Rechazada:
                            _msj = GeneralFunctions.msj_Rechazada;
                            reject = " Razon de Rechazo:";
                            break;
                    }
                    if (Ansal_RazonRechazo == GeneralFunctions.stringDefault) { Ansal_RazonRechazo = null; };
                    Result = Function.LeerDatos(out ErrorEmail, tbAnticipoSalario.Ansal_Correlativo, GetEmployee.emp_Nombres, "", GetEmployee.emp_CorreoElectronico, _msj, reject+" "+ Ansal_RazonRechazo);

                    if (!Result) Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorEmail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Function.BitacoraErrores("AnticipoViatico", "UpdateState", UserName, ex.Message.ToString());
                return false;
            }
        }

        // GET: AnticipoSalario/Approve/5
        [HttpPost]
        public JsonResult Approve(int? id)
        {
            var list = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
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
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        } 
        

        // GET: AnticipoSalario/Approve/5   
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
                    TempData["swalfunction"] = GeneralFunctions.sol_Rechazada;
                }
            }
            if (tbAnticipoSalario == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
                //return HttpNotFound();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
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
