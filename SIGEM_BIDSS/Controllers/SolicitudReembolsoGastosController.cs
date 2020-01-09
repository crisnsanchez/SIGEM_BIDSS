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
    [SessionManager]
    public class SolicitudReembolsoGastosController : BaseController

    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: SolicitudReembolsoGastos
        public ActionResult Index()
        {
            var tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Include(t => t.tbEmpleado).Include(t => t.tbEstado);
            return View(tbSolicitudReembolsoGastos.ToList());
        }

        // GET: Municipio
        [HttpPost]
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = (from x in db.tbMunicipio where x.dep_codigo == CodDepartamento select new { mun_codigo = x.mun_codigo, mun_nombre = x.mun_nombre }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        // GET: SolicitudReembolsoGastos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Find(id);
            if (tbSolicitudReembolsoGastos == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastos);
        }

        // GET: SolicitudReembolsoGastos/Create
        public ActionResult Create()
        {

            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = new tbSolicitudReembolsoGastos();
            tbSolicitudReembolsoGastos.Reemga_Id = tbSolicitudReembolsoGastos.Reemga_Id;
            string ErrorMessage = "", UserName = ""; ;
            try
            {
                int EmployeeID = Function.GetUser(out UserName);

                int Year = Function.DatetimeNow().Year;
                int Month = Function.DatetimeNow().Month;

                int SolCount = (from _tbSol in db.tbSolicitudReembolsoGastos where _tbSol.Reemga_FechaCrea.Year == Year && _tbSol.emp_Id == EmployeeID select _tbSol).Count();
                int SolCountMonth = (from _tbSol in db.tbSolicitudReembolsoGastos where _tbSol.emp_Id == EmployeeID && _tbSol.Reemga_FechaCrea.Month == Month && _tbSol.Reemga_FechaCrea.Year == Year select _tbSol).Count();

                var _Parameters = db.tbParametro.FirstOrDefault();
                if (_Parameters == null)
                {
                    return RedirectToAction("Create", "Parametro");
                }
                if (SolCount >= _Parameters.par_FrecuenciaAdelantoSalario)
                {
                    TempData["swalfunction"] = GeneralFunctions.YearLimit;
                    return RedirectToAction("Solicitud", "Menu");
                }
                if (SolCount >= 6)
                {
                    TempData["swalfunction"] = GeneralFunctions.MonthLimit;
                    return RedirectToAction("Solicitud", "Menu");
                }
                IEnumerable<object> Employee = (from _tbEmp in db.tbEmpleado
                                                where _tbEmp.emp_EsJefe == true && _tbEmp.est_Id == GeneralFunctions.empleadoactivo && _tbEmp.emp_Id != EmployeeID
                                                select new { emp_Id = _tbEmp.emp_Id, emp_Nombres = _tbEmp.emp_Nombres + " " + _tbEmp.emp_Apellidos }).ToList();




                ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
                ViewBag.Reemga_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
                ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");

            }
            catch (Exception Ex)
            {
                ErrorMessage = Ex.Message.ToString();
                throw;
            }

            return View(tbSolicitudReembolsoGastos);

        }

        // POST: SolicitudReembolsoGastos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_Id,Reemga_JefeInmediato,Reemga_GralFechaSolicitud," +
            "Reemga_FechaViaje,Reemga_Cliente,mun_codigo,Reemga_PropositoVisita,Reemga_DiasVisita,Reemga_Comentario,est_Id")] tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos, string dep_codigo)
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

                ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
                ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");

                ViewBag.Reemga_JefeInmediato = new SelectList(Employee, "emp_Id", "emp_Nombres");
                ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
                tbSolicitudReembolsoGastos.Reemga_RazonRechazo = GeneralFunctions.stringDefault;



                tbSolicitudReembolsoGastos.emp_Id = EmployeeID;
                tbSolicitudReembolsoGastos.Reemga_GralFechaSolicitud = Function.DatetimeNow();
                tbSolicitudReembolsoGastos.est_Id = GeneralFunctions.Enviada;




                if (tbSolicitudReembolsoGastos.mun_codigo == "Seleccione")
                    ModelState.AddModelError("mun_codigo", "El campo Municipio es obligatorio.");
                else
                    ViewBag.muncodigo = tbSolicitudReembolsoGastos.mun_codigo;


                if (String.IsNullOrEmpty(dep_codigo))
                    ModelState.AddModelError("Reemga_UsuarioCrea", "El campo Departamento es obligatorio.");




                if (ModelState.IsValid)
                {


                    Insert = db.UDP_Adm_tbSolicitudReembolsoGastos_Insert(tbSolicitudReembolsoGastos.emp_Id,
                        tbSolicitudReembolsoGastos.Reemga_JefeInmediato,
                        tbSolicitudReembolsoGastos.Reemga_GralFechaSolicitud,
                        tbSolicitudReembolsoGastos.Reemga_FechaViaje,
                        tbSolicitudReembolsoGastos.Reemga_Cliente,
                        tbSolicitudReembolsoGastos.mun_codigo,
                        tbSolicitudReembolsoGastos.Reemga_PropositoVisita,
                        tbSolicitudReembolsoGastos.Reemga_DiasVisita,
                        tbSolicitudReembolsoGastos.Reemga_Comentario,
                        tbSolicitudReembolsoGastos.est_Id,
                        tbSolicitudReembolsoGastos.Reemga_RazonRechazo,
                        EmployeeID,
                        Function.DatetimeNow());
                    foreach (UDP_Adm_tbSolicitudReembolsoGastos_Insert_Result Reembolso in Insert)
                        ErrorMessage = Reembolso.MensajeError;



                    if (ErrorMessage.StartsWith("-1"))
                    {

                        Function.BitacoraErrores("SolicitudReembolsoGastos", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");

                        return View(tbSolicitudReembolsoGastos);
                    }

                    else
                    {
                        GetEmployee = Function.GetUserInfo(EmployeeID);
                        var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, GetEmployee.emp_CorreoElectronico, GeneralFunctions.msj_Enviada, GeneralFunctions.stringEmpty);
                        ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GeneralFunctions.stringEmpty, GetEmployee.emp_Nombres, _Parameters.par_CorreoEmpresa, GeneralFunctions.msj_ToAdmin, GeneralFunctions.stringEmpty);


                        if (!Result) Function.BitacoraErrores("SolicitudReembolsoGastos", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Function.BitacoraErrores("SolicitudReembolsoGastos", "CreatePost", UserName, ErrorEmail);
                        TempData["swalfunction"] = "true";

                        Session["Reemga_Id"] = ErrorMessage;
                        return RedirectToAction("Create", "SolicitudReembolsoGastosDetalles");
                    
                    }

                }
            }


            catch (Exception ex)
            {
                Function.BitacoraErrores("SolicitudReembolsoGastos", "CreatePost", UserName, ex.Message.ToString());
                return RedirectToAction("Create", "SolicitudReembolsoGastosDetalle");
            }
            return RedirectToAction("Create", "SolicitudReembolsoGastosDetalle");
      

        }


        // GET: SolicitudReembolsoGastos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Find(id);
            if (tbSolicitudReembolsoGastos == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSolicitudReembolsoGastos.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbSolicitudReembolsoGastos.est_Id);
            return View(tbSolicitudReembolsoGastos);
        }

        // POST: SolicitudReembolsoGastos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Reemga_Id,Reemga_Correlativo,emp_Id,emp_EsJefe,Reemga_GralFechaSolicitud,Reemga_FechaViaje,Reemga_Cliente,mun_codigo,Reemga_PropositoVisita,Reemga_DiasVisita,Reemga_Comentario,est_Id,Reemga_RazonRechazo,Reemga_UsuarioModifica,Reemga_FechaModifica")] tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitudReembolsoGastos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSolicitudReembolsoGastos.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbSolicitudReembolsoGastos.est_Id);
            return View(tbSolicitudReembolsoGastos);
        }

        // GET: SolicitudReembolsoGastos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Find(id);
            if (tbSolicitudReembolsoGastos == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastos);
        }

        // POST: SolicitudReembolsoGastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Find(id);
            db.tbSolicitudReembolsoGastos.Remove(tbSolicitudReembolsoGastos);
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
