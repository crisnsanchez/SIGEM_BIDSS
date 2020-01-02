using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IdentityModel.Claims;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class AnticipoViaticoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Funtion = new GeneralFunctions();

        // GET: AnticipoViatico
        public ActionResult Index()
        {
            try
            {
                var tbAnticipoViatico = db.tbAnticipoViatico.Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbMunicipio).Include(t => t.tbTipoTransporte);
                return View(tbAnticipoViatico.ToList());
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // GET: AnticipoViatico/Details/5
        public ActionResult Details(int? id)
        {
            string vReturn = "";
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoViatico tbAnticipoViatico = db.tbAnticipoViatico.Find(id);
            if (tbAnticipoViatico.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbAnticipoViatico, GeneralFunctions.Revisada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                }
            }
            if (tbAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbAnticipoViatico);
        }

        // GET: AnticipoViatico/Create
        public ActionResult Create()
        {
            tbAnticipoViatico tbAnticipoViatico = new tbAnticipoViatico();
            string ErrorMessage = "";
            try
            {
                string UserName = "";
                int EmployeeID = Funtion.GetUser(out UserName);
                int fecha = Funtion.DatetimeNow().Year;
                int SolCount = (from _tbSol in db.tbAnticipoViatico where _tbSol.Anvi_FechaCrea.Year == fecha && _tbSol.emp_Id == EmployeeID select _tbSol).Count();


                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado where _tbEmp.emp_EsJefe == true select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();



                ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                ViewBag.Anvi_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
                ViewBag.Anvi_tptran_Id = new SelectList(db.tbTipoTransporte, "tptran_Id", "tptran_Descripcion");



            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message.ToString();
                throw;
            }
            return View(tbAnticipoViatico);
        }

        // POST: AnticipoViatico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Anvi_Id,Anvi_Correlativo,emp_Id,Anvi_JefeInmediato,Anvi_GralFechaSolicitud,Anvi_FechaViaje,Anvi_Cliente,mun_Codigo,Anvi_PropositoVisita,Anvi_DiasVisita,Anvi_Hospedaje,Anvi_tptran_Id,Anvi_Autorizacion,Anvi_Comentario,est_Id,Anvi_RazonRechazo,Anvi_UsuarioCrea,Anvi_FechaCrea,Anvi_UsuarioModifica,Anvi_FechaModifica")] tbAnticipoViatico tbAnticipoViatico, string dep_codigo)
        {
            string UserName = "",
                   ErrorEmail = "";

            try
            {
                bool Result = false, ResultAdm = false;
                int EmployeeID = Funtion.GetUser(out UserName);
                tbAnticipoViatico.emp_Id = EmployeeID;
                tbAnticipoViatico.Anvi_GralFechaSolicitud = Funtion.DatetimeNow();
                tbAnticipoViatico.est_Id = GeneralFunctions.Enviada;

                var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();

                if (tbAnticipoViatico.mun_Codigo == "Seleccione")
                    ModelState.AddModelError("mun_codigo", "El campo Municipio es obligatorio.");
                else
                    ViewBag.munCodigo = tbAnticipoViatico.mun_Codigo;

                if (String.IsNullOrEmpty(dep_codigo))
                    ModelState.AddModelError("Anvi_UsuarioCrea", "El campo Departamento es obligatorio.");

                if (ModelState.IsValid)
                {
                    IEnumerable<object> Insert = null;
                    string ErrorMessage = "";
                    if (dep_codigo == "ajax")
                        tbAnticipoViatico.Anvi_Autorizacion = true;
                    Insert = db.UDP_Adm_tbAnticipoViatico_Insert(EmployeeID,
                                                                tbAnticipoViatico.Anvi_JefeInmediato,
                                                                Funtion.DatetimeNow(),
                                                                tbAnticipoViatico.Anvi_FechaViaje,
                                                                tbAnticipoViatico.Anvi_Cliente.ToUpper(),
                                                                tbAnticipoViatico.mun_Codigo,
                                                                tbAnticipoViatico.Anvi_PropositoVisita,
                                                                tbAnticipoViatico.Anvi_DiasVisita,
                                                                tbAnticipoViatico.Anvi_Hospedaje,
                                                                tbAnticipoViatico.Anvi_tptran_Id,
                                                                tbAnticipoViatico.Anvi_Autorizacion,
                                                                tbAnticipoViatico.Anvi_Comentario,
                                                                GeneralFunctions.Enviada,
                                                                EmployeeID,
                                                                Funtion.DatetimeNow());
                    foreach (UDP_Adm_tbAnticipoViatico_Insert_Result Res in Insert)
                        ErrorMessage = Res.MensajeError;

                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Funtion.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    }
                    else
                    {
                        var EmpJefe = db.tbEmpleado.Where(x => x.emp_Id == tbAnticipoViatico.Anvi_JefeInmediato).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                        var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();

                        Result = Funtion.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, "", GetEmployee.emp_CorreoElectronico, GeneralFunctions.msj_Enviada, "");
                        ResultAdm = Funtion.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GetEmployee.emp_Nombres, _Parameters.par_CorreoEmpresa, GeneralFunctions.msj_ToAdmin, "");

                        if (!Result) Funtion.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Funtion.BitacoraErrores("AnticipoSalario", "CreatePost", UserName, ErrorEmail);

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
            ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre", dep_codigo);
            ViewBag.Anvi_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoViatico.Anvi_JefeInmediato);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbAnticipoViatico.mun_Codigo);
            ViewBag.Anvi_tptran_Id = new SelectList(db.tbTipoTransporte, "tptran_Id", "tptran_Descripcion", tbAnticipoViatico.Anvi_tptran_Id);
            return View(tbAnticipoViatico);
        }

        // GET: AnticipoViatico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoViatico tbAnticipoViatico = db.tbAnticipoViatico.Find(id);
            if (tbAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoViatico.emp_Id);
            ViewBag.Anvi_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoViatico.Anvi_JefeInmediato);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbAnticipoViatico.mun_Codigo);
            ViewBag.Anvi_tptran_Id = new SelectList(db.tbTipoTransporte, "tptran_Id", "tptran_Descripcion", tbAnticipoViatico.Anvi_tptran_Id);
            return View(tbAnticipoViatico);
        }

        // POST: AnticipoViatico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Anvi_Id,emp_Id,Anvi_JefeInmediato,Anvi_Comentario,Anvi_GralFechaSolicitud,Anvi_FechaViaje,Anvi_Cliente,mun_Codigo,Anvi_PropositoVisita,Anvi_DiasVisita,Anvi_Hospedaje,Anvi_tptran_Id,Anvi_Autorizacion,Anvi_UsuarioCrea,Anvi_FechaCrea,Anvi_UsuarioModifica,Anvi_FechaModifica")] tbAnticipoViatico tbAnticipoViatico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAnticipoViatico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoViatico.emp_Id);
            ViewBag.Anvi_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAnticipoViatico.Anvi_JefeInmediato);
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbAnticipoViatico.mun_Codigo);
            ViewBag.Anvi_tptran_Id = new SelectList(db.tbTipoTransporte, "tptran_Id", "tptran_Descripcion", tbAnticipoViatico.Anvi_tptran_Id);
            return View(tbAnticipoViatico);
        }

        // GET: Municipio
        [HttpPost]
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = (from x in db.tbMunicipio where x.dep_codigo == CodDepartamento select new { mun_codigo = x.mun_codigo, mun_nombre = x.mun_nombre }).ToList();
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
        /////////////////////////////////////////////////////////       


        [HttpPost]
        public JsonResult Revisar(int id, string Anvi_RazonRechazo)
        {
            string vReturn = "";
            string IsFor = "false";
            tbAnticipoViatico tbAnticipoViatico = db.tbAnticipoViatico.Find(id);
            if (tbAnticipoViatico.est_Id == GeneralFunctions.Enviada)
            {
                if (UpdateState(out vReturn, tbAnticipoViatico, GeneralFunctions.Revisada, Anvi_RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Revisada;
                    IsFor = "true";
                }
            }
            if (tbAnticipoViatico == null)
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
            tbAnticipoViatico tbAnticipoViatico = db.tbAnticipoViatico.Find(id);
            if (tbAnticipoViatico.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbAnticipoViatico, GeneralFunctions.Aprobada, GeneralFunctions.stringDefault))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Aprobada;
                    list = vReturn;
                }
            }
            if (tbAnticipoViatico == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: AnticipoSalario/Approve/5   
        [HttpPost]
        public JsonResult Rejects(int id, string Anvi_RazonRechazo)
        {
            var list = "";
            string vReturn = "";
            if (id == null)
            {
                return Json("Valor Nulo", JsonRequestBehavior.AllowGet);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoViatico tbAnticipoViatico = db.tbAnticipoViatico.Find(id);
            if (tbAnticipoViatico.est_Id == GeneralFunctions.Revisada)
            {
                if (UpdateState(out vReturn, tbAnticipoViatico, GeneralFunctions.Rechazada, Anvi_RazonRechazo))
                {
                    TempData["swalfunction"] = GeneralFunctions.sol_Rechazada;
                }
            }
            if (tbAnticipoViatico == null)
            {
                return Json("Error al cargar datos", JsonRequestBehavior.AllowGet);
                //return HttpNotFound();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public bool UpdateState(out string pvReturn, tbAnticipoViatico tbAnticipoViatico, int State, string Anvi_RazonRechazo)
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
                int EmployeeID = Funtion.GetUser(out UserName);
                tbAnticipoViatico.est_Id = State;
                tbAnticipoViatico.Anvi_RazonRechazo = Anvi_RazonRechazo;
                var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == tbAnticipoViatico.emp_Id).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();

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
                if (Anvi_RazonRechazo == GeneralFunctions.stringDefault) { Anvi_RazonRechazo = null; };
                Result = Funtion.LeerDatos(out ErrorEmail, tbAnticipoViatico.Anvi_Correlativo, GetEmployee.emp_Nombres, "", GetEmployee.emp_CorreoElectronico, _msj, reject + " " + Anvi_RazonRechazo);

                if (!Result)
                {
                    Funtion.BitacoraErrores("AnticipoSalario", "UpdateState", UserName, ErrorEmail);
                    return false;
                }
                else
                {
                    Update = db.UDP_Adm_tbAnticipoViatico_Update(tbAnticipoViatico.Anvi_Id,
                                                                    tbAnticipoViatico.emp_Id,
                                                                    tbAnticipoViatico.Anvi_JefeInmediato,
                                                                   tbAnticipoViatico.Anvi_GralFechaSolicitud,
                                                                   tbAnticipoViatico.Anvi_FechaViaje,
                                                                   tbAnticipoViatico.Anvi_Cliente,
                                                                   tbAnticipoViatico.mun_Codigo,
                                                                   tbAnticipoViatico.Anvi_PropositoVisita,
                                                                   tbAnticipoViatico.Anvi_DiasVisita,
                                                                   tbAnticipoViatico.Anvi_Hospedaje,
                                                                   tbAnticipoViatico.Anvi_tptran_Id,
                                                                   tbAnticipoViatico.Anvi_Autorizacion,
                                                                   tbAnticipoViatico.Anvi_Comentario,
                                                                   tbAnticipoViatico.est_Id,
                                                                   tbAnticipoViatico.Anvi_RazonRechazo,     
                                                               EmployeeID,
                                                                Funtion.DatetimeNow());
                    foreach (UDP_Adm_tbAnticipoViatico_Update_Result Res in Update)
                        ErrorMessage = Res.MensajeError;
                    pvReturn = ErrorMessage;
                    if (ErrorMessage.StartsWith("-1"))
                    {

                        Funtion.BitacoraErrores("AnticipoViatico", "UpdateState", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo actualizar el registro contacte al administrador.");
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                pvReturn = ex.Message.ToString();
                Funtion.BitacoraErrores("AnticipoViatico", "UpdateState", UserName, ex.Message.ToString());
                return false;
            }
        }
    }
}
