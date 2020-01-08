using System;
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
    [Authorize]
    [SessionManager]
    public class RequisionCompraController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: RequisionCompra
        public async Task<ActionResult> Index()
        {
            var tbRequisionCompra = db.tbRequisionCompra.Include(t => t.tbArea).Include(t => t.tbEmpleado).Include(t => t.tbEstado);
            return View(await tbRequisionCompra.ToListAsync());
        }

        // GET: RequisionCompra/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            string vReturn = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            if (tbRequisionCompra.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbRequisionCompra, GeneralFunctions.Revisada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                }
                else
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_ErrorUpdateState;
                }
            }
            if (tbRequisionCompra == null)
            {
                return HttpNotFound();
            }
            return View(tbRequisionCompra);
          
            return View(tbRequisionCompra);
        }

        // GET: RequisionCompra/Create
        public ActionResult Create()
        {
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            return View();
        }

        // POST: RequisionCompra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "are_Id,Reqco_GralFechaSolicitud,Reqco_Comentario,Reqco_RazonRechazo")] tbRequisionCompra tbRequisionCompra)
        {
            string UserName = "", ErrorEmail = "", ErrorMessage = "";
            bool Result = false, ResultAdm = false;
            IEnumerable<object> Insert = null;

            try
            {
                cGetUserInfo GetEmployee = null;
                int EmployeeID = Function.GetUser(out UserName);

                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                                where _tbEmp.emp_EsJefe == true && _tbEmp.est_Id == GeneralFunctions.empleadoactivo && _tbEmp.emp_Id != EmployeeID
                                                select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();


                ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbRequisionCompra.are_Id);
                ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbRequisionCompra.emp_Id);
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbRequisionCompra.est_Id);

                tbRequisionCompra.emp_Id = EmployeeID;
                tbRequisionCompra.Reqco_GralFechaSolicitud = Function.DatetimeNow();
                tbRequisionCompra.est_Id = GeneralFunctions.Enviada;

                var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();


                if (String.IsNullOrEmpty(tbRequisionCompra.Reqco_Comentario)) { tbRequisionCompra.Reqco_Comentario = GeneralFunctions.stringDefault; }

                if (ModelState.IsValid)
                {

                    Insert = db.UDP_Adm_tbRequisionCompra_Insert(EmployeeID,
                                                                tbRequisionCompra.are_Id,
                                                                Function.DatetimeNow(),
                                                                tbRequisionCompra.Reqco_Comentario,
                                                                tbRequisionCompra.est_Id,
                                                                tbRequisionCompra.Reqco_RazonRechazo,
                                                                EmployeeID,
                                                                Function.DatetimeNow());
                    foreach (UDP_Adm_tbRequisionCompraDetalle_Insert_Result Res in Insert)
                        ErrorMessage = Res.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("RequisionCompra", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    }
                    else
                    {
                        GetEmployee = Function.GetUserInfo(EmployeeID);

                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, GeneralFunctions.msj_Enviada, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, GetEmployee.emp_CorreoElectronico);
                        ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GetEmployee.emp_Nombres, GeneralFunctions.msj_ToAdmin, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, _Parameters.par_CorreoRRHH);

                        if (!Result) Function.BitacoraErrores("RequisionCompra", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Function.BitacoraErrores("RequisionCompra", "CreatePost", UserName, ErrorEmail);

                        TempData["swalfunction"] = GeneralFunctions.sol_Enviada;
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                Function.BitacoraErrores("RequisionCompra", "CreatePost", UserName, ex.Message.ToString()+ " " + ex.InnerException.Message.ToString());
            }
            return View(tbRequisionCompra);
        }


        public bool UpdateState(out string pvReturn, tbRequisionCompra tbRequisionCompra, int State, string RazonRechazo)
        {
            pvReturn = "";
            string UserName = "", ErrorEmail = "", ErrorMessage = "", _msj = "", _msjFor = "";
            bool Result = false, ResultFor = false;
            IEnumerable<object> Update = null;
            var reject = "";

            try
            {
                int EmployeeID = Function.GetUser(out UserName);

                tbRequisionCompra.est_Id = State;

                tbRequisionCompra.Reqco_RazonRechazo = RazonRechazo;

                Update = db.UDP_Adm_tbRequisionCompra_Update(tbRequisionCompra.Reqco_Id,
                                                                              tbRequisionCompra.est_Id,
                                                                              tbRequisionCompra.Reqco_RazonRechazo,
                                                                              EmployeeID,
                                                                              Function.DatetimeNow());
                foreach (UDP_Adm_tbRequisionCompra_Update_Result Res in Update)
                    ErrorMessage = Res.MensajeError;
                pvReturn = ErrorMessage;
                if (ErrorMessage.StartsWith("-1"))
                {

                    Function.BitacoraErrores("RequisionCompra", "UpdateState", UserName, ErrorMessage);
                    ModelState.AddModelError("", "No se pudo actualizar el registro contacte al administrador.");
                    return false;
                }
                else
                {
                    var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                    var GetEmployee = Function.GetUserInfo(tbRequisionCompra.emp_Id);
                    var Approver = Function.GetUserInfo(EmployeeID);
                    string Correlativo = tbRequisionCompra.Reqco_Correlativo;
                    string Nombres = GetEmployee.emp_Nombres;
                    string CorreoElectronico = GetEmployee.emp_CorreoElectronico;
                    string sApprover = Approver.strFor + Approver.emp_Nombres;
                    if (RazonRechazo == GeneralFunctions.stringDefault) { RazonRechazo = null; };
                    _msjFor = GeneralFunctions.msj_ToAdmin;
                    switch (State)
                    {
                        case GeneralFunctions.Revisada:
                            _msj = GeneralFunctions.msj_Revisada;
                            break;
                        case GeneralFunctions.AprobadaPorJefe:
                            _msj = GeneralFunctions.msj_RevisadaPorJefe;
                            ResultFor = Function.LeerDatos(out ErrorEmail, Correlativo, _Parameters.par_NombreEmpresa, Nombres, _msjFor, GeneralFunctions.stringEmpty, sApprover, _Parameters.par_CorreoEmpresa);
                            break;
                        case GeneralFunctions.AprobadaPorRRHH:
                            _msj = GeneralFunctions.msj_RevisadaPorRRHH;
                            ResultFor = Function.LeerDatos(out ErrorEmail, Correlativo, _Parameters.par_NombreEmpresa, Nombres, _msjFor, GeneralFunctions.stringEmpty, sApprover, _Parameters.par_CorreoEmpresa);
                            break;
                        case GeneralFunctions.AprobadaPorAdmin:
                            _msj = GeneralFunctions.msj_RevisadaPorAdmin;
                            ResultFor = Function.LeerDatos(out ErrorEmail, Correlativo, _Parameters.par_NombreEmpresa, Nombres, _msjFor, GeneralFunctions.stringEmpty, sApprover, _Parameters.par_CorreoEmpresa);
                            break;
                        case GeneralFunctions.Aprobada:
                            _msj = GeneralFunctions.msj_Aprobada;
                            break;
                        case GeneralFunctions.Rechazada:
                            _msj = GeneralFunctions.msj_Rechazada;
                            reject = " Razon de Rechazo:";
                            break;
                    }
                    Result = Function.LeerDatos(out ErrorEmail, Correlativo, Nombres, GeneralFunctions.stringEmpty, _msj, reject + " " + RazonRechazo, sApprover, CorreoElectronico);


                    if (!Result) { Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorEmail); return false; }
                    if (!ResultFor) { Function.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorEmail); return false; }

                }
                return true;

            }
            catch (Exception ex)
            {
                pvReturn = ex.Message.ToString();
                Function.BitacoraErrores("AnticipoViatico", "UpdateState", UserName, ex.Message.ToString());
                return false;
            }
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
