﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class AccionPersonalController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        GeneralFunctions Function = new GeneralFunctions();

        // GET: AccionPersonal
        public async Task<ActionResult> Index()
        {
            var tbAccionPersonal = db.tbAccionPersonal.Include(t => t.tbEstado).Include(t => t.tbEmpleado).Include(t => t.tbTipoMovimiento).Include(t => t.tbEmpleado1);
            return View(await tbAccionPersonal.ToListAsync());
        }

        // GET: AccionPersonal/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            string vReturn = "";
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAccionPersonal tbAccionPersonal = db.tbAccionPersonal.Find(id);
            if (tbAccionPersonal.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbAccionPersonal, GeneralFunctions.Revisada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                }
            }
            if (tbAccionPersonal == null)
            {
                return HttpNotFound();
            }
            return View(tbAccionPersonal);
        }
        // GET: AccionPersonal/Create
        public ActionResult Create()
        {
            tbAccionPersonal tbAccionPersonal = new tbAccionPersonal();
            string ErrorMessage = "";
            try
            {
                string UserName = "";
                int EmployeeID = Function.GetUser(out UserName);
                int fecha = Function.DatetimeNow().Year;
                int SolCount = (from _tbSol in db.tbAccionPersonal where _tbSol.Acp_FechaCrea.Year == fecha && _tbSol.emp_Id == EmployeeID select _tbSol).Count();


                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.emp_EsJefe == true select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();



                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
              
                ViewBag.Acp_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
                ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
                ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
                tbAccionPersonal.Acp_FechaSolicitud = Function.DatetimeNow();


            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message.ToString();
                throw;
            }
            return View(tbAccionPersonal);
        }
    

    // POST: AccionPersonal/Create
    // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
    // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Acp_Id,Acp_Correlativo,emp_Id,Acp_JefeInmediato,Acp_FechaSolicitud,tipmo_id,Acp_Comentario,est_Id,Acp_RazonRechazo,Acp_UsuarioCrea,Acp_FechaCrea,Acp_UsuarioModifica,Acp_FechaModifica")] tbAccionPersonal tbAccionPersonal)
    {
        string UserName = "",
            ErrorEmail = "";

        try
        {
            bool Result = false, ResultAdm = false;
            int EmployeeID = Function.GetUser(out UserName);
            tbAccionPersonal.emp_Id = EmployeeID;
            tbAccionPersonal.Acp_FechaSolicitud = Function.DatetimeNow();
            tbAccionPersonal.est_Id = GeneralFunctions.Enviada;

            var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();

            
            if (ModelState.IsValid)
            {
                IEnumerable<object> Insert = null;
                string ErrorMessage = "";
             
                 
                Insert = db.UDP_Adm_tbAccionPersonal_Insert(EmployeeID,
                                                            tbAccionPersonal.Acp_JefeInmediato,
                                                            Function.DatetimeNow(),
                                                            tbAccionPersonal.Acp_Comentario,
                                                             GeneralFunctions.Enviada,
                                                             tbAccionPersonal.tipmo_id,
                                                             EmployeeID,
                                                             Function.DatetimeNow()
                                                            );
                foreach (UDP_Adm_tbAccionPersonal_Insert_Result Res in Insert)
                    ErrorMessage = Res.MensajeError;

                if (ErrorMessage.StartsWith("-1"))
                {
                        Function.BitacoraErrores("AccionPersonal", "CreatePost", UserName, ErrorMessage);
                    ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                }
                else
                {
                    var EmpJefe = db.tbEmpleado.Where(x => x.emp_Id == tbAccionPersonal.Acp_JefeInmediato).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                    var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();

                    Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, GetEmployee.emp_CorreoElectronico, GeneralFunctions.msj_Enviada, GeneralFunctions.stringEmpty);
                    ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GeneralFunctions.stringEmpty, GetEmployee.emp_Nombres, _Parameters.par_CorreoEmpresa, GeneralFunctions.msj_ToAdmin, GeneralFunctions.stringEmpty);


                    if (!Result) Function.BitacoraErrores("AccionPersonal", "CreatePost", UserName, ErrorEmail);
                    if (!ResultAdm) Function.BitacoraErrores("AccionPersonal", "CreatePost", UserName, ErrorEmail);

                    TempData["swalfunction"] = GeneralFunctions.sol_Enviada;
                    return RedirectToAction("Index");
                }

            }
        }
        catch (Exception ex)
        {
                Function.BitacoraErrores("AccionPersonal", "CreatePost", UserName, ex.Message.ToString());
        }
        IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                        where _tbEmp.emp_EsJefe == true
                                        select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();
        ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");

            ViewBag.Acp_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            return View(tbAccionPersonal);
    }


        // GET: AccionPersonal/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAccionPersonal tbAccionPersonal = await db.tbAccionPersonal.FindAsync(id);
            if (tbAccionPersonal == null)
            {
                return HttpNotFound();
            }
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbAccionPersonal.est_Id);
            ViewBag.Acp_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.Acp_JefeInmediato);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbAccionPersonal.tipmo_id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            return View(tbAccionPersonal);
        }

        // POST: AccionPersonal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Acp_Id,Acp_Correlativo,emp_Id,Acp_JefeInmediato,Acp_FechaSolicitud,tipmo_id,Acp_Comentario,est_Id,Acp_RazonRechazo,Acp_UsuarioCrea,Acp_FechaCrea,Acp_UsuarioModifica,Acp_FechaModifica")] tbAccionPersonal tbAccionPersonal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAccionPersonal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbAccionPersonal.est_Id);
            ViewBag.Acp_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.Acp_JefeInmediato);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbAccionPersonal.tipmo_id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            return View(tbAccionPersonal);
        }

        // GET: AccionPersonal/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAccionPersonal tbAccionPersonal = await db.tbAccionPersonal.FindAsync(id);
            if (tbAccionPersonal == null)
            {
                return HttpNotFound();
            }
            return View(tbAccionPersonal);
        }

        // POST: AccionPersonal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbAccionPersonal tbAccionPersonal = await db.tbAccionPersonal.FindAsync(id);
            db.tbAccionPersonal.Remove(tbAccionPersonal);
            await db.SaveChangesAsync();
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

        /////////////////////////////////////////////////////////  
        public bool UpdateState(out string pvReturn, tbAccionPersonal tbAccionPersonal, int State, string RazonRechazo)
        {
            pvReturn = "";
            string UserName = "",
                ErrorEmail = "",
                ErrorMessage = "",
                _msj = "",
                reject = "";
            bool Result = false;
            IEnumerable<object> Update = null;
            cGetUserInfo GetEmployee = null, Approver = null;
            try
            {
                int EmployeeID = Function.GetUser(out UserName);
                tbAccionPersonal.est_Id = State;
                tbAccionPersonal.Acp_RazonRechazo = RazonRechazo;
                Update = db.UDP_Adm_tbAccionPersonal_Update(tbAccionPersonal.Acp_Id,
                                                                tbAccionPersonal.est_Id,
                                                                tbAccionPersonal.Acp_RazonRechazo,
                                                                EmployeeID,
                                                                Function.DatetimeNow());
                foreach (UDP_Adm_tbAccionPersonal_Update_Result Res in Update)
                    ErrorMessage = Res.MensajeError;
                pvReturn = ErrorMessage;
                if (ErrorMessage.StartsWith("-1"))
                {
                    Function.BitacoraErrores("AccionPersonal", "UpdateState", UserName, ErrorMessage);
                    ModelState.AddModelError("", "No se pudo actualizar el registro contacte al administrador.");
                    return false;
                }
                else
                {
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
                    if (RazonRechazo == GeneralFunctions.stringDefault) { RazonRechazo = null; };

                    GetEmployee = Function.GetUserInfo(tbAccionPersonal.emp_Id);
                    Approver = Function.GetUserInfo(EmployeeID);
                    if (RazonRechazo == GeneralFunctions.stringDefault) { RazonRechazo = null; };

                    Result = Function.LeerDatos(out ErrorEmail, tbAccionPersonal.Acp_Correlativo, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, _msj, reject + " " + RazonRechazo, Approver.strFor + Approver.emp_Nombres, GetEmployee.emp_CorreoElectronico);

                    if (!Result)
                    {
                        Function.BitacoraErrores("AccionPersonal", "UpdateState", UserName, ErrorEmail);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                pvReturn = ex.Message.ToString();
                Function.BitacoraErrores("AccionPersonal","UpdateState", UserName, ex.Message.ToString());
                return false;
            }
        }



        [HttpPost]
        public JsonResult Revisar(int id, string RazonRechazo)
        {
            string vReturn = "";
            string IsFor = "false";
            tbAccionPersonal tbAccionPersonal = db.tbAccionPersonal.Find(id);
            if (tbAccionPersonal.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbAccionPersonal, GeneralFunctions.Revisada, RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                    IsFor = "true";
                }
            }
            if (tbAccionPersonal == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(IsFor, JsonRequestBehavior.AllowGet);
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
            tbAccionPersonal tbAccionPersonal = db.tbAccionPersonal.Find(id);
            if (tbAccionPersonal.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbAccionPersonal, GeneralFunctions.Aprobada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                    list = vReturn;
                }
            }
            if (tbAccionPersonal == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        // GET: AnticipoSalario/Approve/5   
        [HttpPost]
        public JsonResult Reject(int id, string RazonRechazo)
        {
            var list = "";
            string vReturn = "";
            tbAccionPersonal tbAccionPersonal = db.tbAccionPersonal.Find(id);
            if (tbAccionPersonal.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbAccionPersonal, GeneralFunctions.Rechazada, RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Rechazada;
                }
            }
            if (tbAccionPersonal == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
                //return HttpNotFound();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



    }
}
