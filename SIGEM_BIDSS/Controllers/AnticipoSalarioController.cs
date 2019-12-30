﻿

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
            string vReturn = "";
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbAnticipoSalario, GeneralFunctions.Revisada, GeneralFunctions.stringDefault))
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
            string vReturn = "";
            string IsFor = "false";
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbAnticipoSalario, GeneralFunctions.Revisada, Ansal_RazonRechazo))
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

        // GET: AnticipoSalario/Revisar
        [HttpPost]
        public JsonResult Calcular(cCalDecimales cCalDecimal)
        {
            
            string spanCantidad = "", MASspanCantidad="";
            if (cCalDecimal.empMonto > cCalDecimal.empSueldo)
            {
                MASspanCantidad = "Monto solicitado mayor que el Sueldo";
            }
           
            if (cCalDecimal.empMonto > cCalDecimal.empPorcetanje)
            {
                spanCantidad = "El monto no puede ser mayor que el pocentaje permitido";
            }
          
            object vCalcular = new { spanCantidad, MASspanCantidad };
            return Json(vCalcular, JsonRequestBehavior.AllowGet);
        }




        // GET: AnticipoSalario/Create
        public ActionResult Create()
        {
            tbAnticipoSalario tbAnticipoSalario = new tbAnticipoSalario();
            string ErrorMessage = "";
            try
            {
                string UserName = "";
                int EmployeeID = Function.GetUser(out UserName);
                int fecha = Function.DatetimeNow().Year;
                int SolCount = (from _tbSol in db.tbAnticipoSalario where _tbSol.Ansal_FechaCrea.Year == fecha && _tbSol.emp_Id == EmployeeID select _tbSol).Count();
                var _Parameters = db.tbParametro.FirstOrDefault();
                if (_Parameters == null)
                {
                    return HttpNotFound();
                }
                if (SolCount >= _Parameters.par_FrecuenciaAdelantoSalario)
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Rechazada;
                    return RedirectToAction("Solicitud", "Menu");
                }
                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.emp_EsJefe == true select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();
                var vSueldo = (from _tbSueldo in db.tbSueldo where _tbSueldo.emp_Id == EmployeeID select _tbSueldo.sue_Cantidad).FirstOrDefault();
                var _percent = vSueldo * (Convert.ToDecimal(_Parameters.par_PorcentajeAdelantoSalario) / 100);

                tbAnticipoSalario.Sueldo = Convert.ToDecimal(vSueldo);
                tbAnticipoSalario.Porcentaje = decimal.Round(Convert.ToDecimal(_percent), 2);

                ViewBag.Ansal_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
                ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message.ToString();
                throw;
            }
            return View(tbAnticipoSalario);
        }

        // POST: AnticipoSalario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ansal_Id,Ansal_Correlativo,emp_Id,Ansal_JefeInmediato,Ansal_GralFechaSolicitud,Ansal_MontoSolicitado,tpsal_id,Ansal_Justificacion,Ansal_Comentario,est_Id,Ansal_RazonRechazo,Cantidad,Sueldo")] tbAnticipoSalario tbAnticipoSalario)
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

                var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                var vSueldo = (from _tbSueldo in db.tbSueldo where _tbSueldo.emp_Id == EmployeeID select _tbSueldo.sue_Cantidad).FirstOrDefault();
                var _percent = vSueldo * (Convert.ToDecimal(_Parameters.par_PorcentajeAdelantoSalario) / 100);

                if (String.IsNullOrEmpty(tbAnticipoSalario.Ansal_Comentario)) { tbAnticipoSalario.Ansal_Comentario = GeneralFunctions.stringDefault; }
                tbAnticipoSalario.Ansal_MontoSolicitado = Convert.ToDecimal(tbAnticipoSalario.Cantidad.Replace(",", ""));

                if (tbAnticipoSalario.Ansal_MontoSolicitado > vSueldo)
                    ModelState.AddModelError("Cantidad", "El monto no puede ser mayor que el sueldo.");

                if (tbAnticipoSalario.Ansal_MontoSolicitado > _percent)
                    ModelState.AddModelError("Cantidad", "El monto no puede ser mayor que el pocentaje permitido.");


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




        public bool UpdateState(out string pvReturn, tbAnticipoSalario tbAnticipoSalario, int State, string Ansal_RazonRechazo)
        {
            string UserName = "",
                ErrorEmail = "";
            bool Result = false;
            pvReturn = "";
            IEnumerable<object> Update = null;
            string ErrorMessage = "";
            string _msj = "";
            var reject = "";

            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                tbAnticipoSalario.est_Id = State;
                tbAnticipoSalario.Ansal_RazonRechazo = Ansal_RazonRechazo;
                var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == tbAnticipoSalario.emp_Id).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();

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
                Result = Function.LeerDatos(out ErrorEmail, tbAnticipoSalario.Ansal_Correlativo, GetEmployee.emp_Nombres, "", GetEmployee.emp_CorreoElectronico, _msj, reject + " " + Ansal_RazonRechazo);

                if (!Result)
                {
                    Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorEmail);
                    return false;
                }
                else
                {
                    Update = db.UDP_Adm_tbAnticipoSalario_Update(tbAnticipoSalario.Ansal_Id,
                                                                tbAnticipoSalario.emp_Id,
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
                    pvReturn = ErrorMessage;
                    if (ErrorMessage.StartsWith("-1"))
                    {

                        Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo actualizar el registro contacte al administrador.");
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                pvReturn = ex.Message.ToString();
                Function.BitacoraErrores("AnticipoViatico", "UpdateState", UserName, ex.Message.ToString());
                return false;
            }
        }

        // GET: AnticipoSalario/Approve/5
        [HttpPost]
        public JsonResult Approve(int? id)
        {
            var list = "";
            string vReturn = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbAnticipoSalario, GeneralFunctions.Aprobada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                    list = vReturn;
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
            string vReturn = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoSalario tbAnticipoSalario = db.tbAnticipoSalario.Find(id);
            if (tbAnticipoSalario.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbAnticipoSalario, GeneralFunctions.Rechazada, Ansal_RazonRechazo))
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
