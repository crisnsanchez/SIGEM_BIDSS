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
    public class VacacionesPermisosEspecialesController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: VacacionesPermisosEspeciales
        public ActionResult Index()
        {
            var tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbEstado).Include(t => t.tbTipoPermiso);
            return View(tbVacacionesPermisosEspeciales.ToList());
        }

        // GET: VacacionesPermisosEspeciales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales = db.tbVacacionesPermisosEspeciales.Find(id);
            if (tbVacacionesPermisosEspeciales == null)
            {
                return HttpNotFound();
            }
            return View(tbVacacionesPermisosEspeciales);
        }


        [HttpPost]
        public JsonResult CalcularFecha(cCalFechas cCalFechas)
        {

            string MASspan = "", MASspanFecha = "";
            if (cCalFechas.FechaInicio > cCalFechas.FechaFin)
            {
                MASspanFecha = "1";
                MASspan = "La Fecha de inicio no puede ser mayor que la final";
            }
            if (cCalFechas.FechaFin < cCalFechas.FechaInicio)
            {
                MASspanFecha = "2";
                MASspan = "La Fecha de finalizacion no puede ser mayor que la inicio";
            }
                object vCalcular = new { MASspan, MASspanFecha };
            return Json(vCalcular, JsonRequestBehavior.AllowGet);
        }





        // GET: VacacionesPermisosEspeciales/Create
        public ActionResult Create()
        {
            string UserName = "";
            int EmployeeID = Function.GetUser(out UserName);
            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.emp_EsJefe == true && _tbEmp.est_Id == GeneralFunctions.empleadoactivo && _tbEmp.emp_Id != EmployeeID select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();


            ViewBag.VPE_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion");
            return View();
        }

        // POST: VacacionesPermisosEspeciales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VPE_JefeInmediato,tperm_Id,VPE_GralFechaSolicitud,VPE_FechaInicio,VPE_FechaFin,VPE_CantidadDias,VPE_MontoSolicitado,VPE_Comentario")] tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales)
        {
            string UserName = "",
              ErrorEmail = "";

            try
            {
                bool Result = false, ResultAdm = false;
                int EmployeeID = Function.GetUser(out UserName);
                tbVacacionesPermisosEspeciales.emp_Id = EmployeeID;
                tbVacacionesPermisosEspeciales.VPE_GralFechaSolicitud = Function.DatetimeNow();
                tbVacacionesPermisosEspeciales.est_Id = GeneralFunctions.Enviada;

                var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                var vSueldo = (from _tbSueldo in db.tbSueldo where _tbSueldo.emp_Id == EmployeeID select _tbSueldo.sue_Cantidad).FirstOrDefault();
                var _percent = vSueldo * (Convert.ToDecimal(_Parameters.par_PorcentajeAdelantoSalario) / 100);

                if (String.IsNullOrEmpty(tbVacacionesPermisosEspeciales.VPE_Comentario)) { tbVacacionesPermisosEspeciales.VPE_Comentario = GeneralFunctions.stringDefault; }
           
                if (ModelState.IsValid)
                {
                    IEnumerable<object> Insert = null;
                    string ErrorMessage = "";

                    Insert = db.UDP_Adm_tbVacacionesPermisosEspeciales_Insert(EmployeeID,
                        tbVacacionesPermisosEspeciales.VPE_JefeInmediato,
                        tbVacacionesPermisosEspeciales.tperm_Id,
                        tbVacacionesPermisosEspeciales.est_Id,
                        Function.DatetimeNow(),
                        tbVacacionesPermisosEspeciales.VPE_FechaInicio,
                        tbVacacionesPermisosEspeciales.VPE_FechaFin,
                        tbVacacionesPermisosEspeciales.VPE_CantidadDias,
                        tbVacacionesPermisosEspeciales.VPE_Comentario,
                        tbVacacionesPermisosEspeciales.VPE_RazonRechazo,
                        EmployeeID,
                        Function.DatetimeNow());

                    foreach (UDP_Adm_tbVacacionesPermisosEspeciales_Insert_Result Res in Insert)
                        ErrorMessage = Res.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    }
                    else
                    {
                        var EmpJefe = db.tbEmpleado.Where(x => x.emp_Id == tbVacacionesPermisosEspeciales.VPE_JefeInmediato).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                        var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();

                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, "", GetEmployee.emp_CorreoElectronico, GeneralFunctions.msj_Enviada, "");
                        ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GetEmployee.emp_Nombres, _Parameters.par_CorreoEmpresa, GeneralFunctions.msj_ToAdmin, "");

                        if (!Result) Function.BitacoraErrores("VacacionesPermisosEspeciales", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Function.BitacoraErrores("VacacionesPermisosEspeciales", "CreatePost", UserName, ErrorEmail);

                        TempData["swalfunction"] = GeneralFunctions.sol_Enviada;
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                Function.BitacoraErrores("VacacionesPermisosEspeciales", "CreatePost", UserName, ex.Message.ToString());
            }
            IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                            where _tbEmp.emp_EsJefe == true
                                            select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbVacacionesPermisosEspeciales.emp_Id);
            ViewBag.VPE_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbVacacionesPermisosEspeciales.VPE_JefeInmediato);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbVacacionesPermisosEspeciales.est_Id);
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion", tbVacacionesPermisosEspeciales.tperm_Id);
            return View(tbVacacionesPermisosEspeciales);
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
