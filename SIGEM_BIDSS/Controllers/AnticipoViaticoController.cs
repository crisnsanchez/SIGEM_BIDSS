using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.IdentityModel.Claims;
using System.IO;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class AnticipoViaticoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: AnticipoViatico
        public ActionResult Index()
        {
            var tbAnticipoViatico = db.tbAnticipoViatico.Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbMunicipio).Include(t => t.tbTipoTransporte);
            return View(tbAnticipoViatico.ToList());
        }

        // GET: AnticipoViatico/Details/5
        public ActionResult Details(int? id)
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
            return View(tbAnticipoViatico);
        }

        // GET: AnticipoViatico/Create
        public ActionResult Create()
        {
            var JefeInmediato = db.tbEmpleado.Select(s => new {
                emp_Id = s.emp_Id,
                emp_Nombres = s.emp_Nombres,
                emp_EsJefe = s.emp_EsJefe
            }).Where(x=>x.emp_EsJefe==true).ToList();

            
            ViewBag.dep_codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            ViewBag.Anvi_JefeInmediato = new SelectList(JefeInmediato, "emp_Id", "emp_Nombres");
            ViewBag.mun_Codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
            ViewBag.Anvi_tptran_Id = new SelectList(db.tbTipoTransporte, "tptran_Id", "tptran_Descripcion");
            return View();
        }

        // POST: AnticipoViatico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Anvi_Id,emp_Id,Anvi_JefeInmediato,Anvi_Comentario,Anvi_GralFechaSolicitud,Anvi_FechaViaje,Anvi_Cliente,mun_Codigo,Anvi_PropositoVisita,Anvi_DiasVisita,Anvi_Hospedaje,Anvi_tptran_Id,Anvi_Autorizacion,Anvi_UsuarioCrea,Anvi_FechaCrea,Anvi_UsuarioModifica,Anvi_FechaModifica")] tbAnticipoViatico tbAnticipoViatico, string dep_codigo)
        {
            string UserName = "",
                   ErrorEmail = "";

            try
            {
                bool Result = false;
                int EmployeeID = Function.GetUser(out UserName);
                tbAnticipoViatico.emp_Id = EmployeeID;
                tbAnticipoViatico.Anvi_GralFechaSolicitud = Function.DatetimeNow();
                tbAnticipoViatico.est_Id = GeneralFunctions.Enviada;

                if (tbAnticipoViatico.mun_Codigo == "Seleccione")
                    ModelState.AddModelError("mun_codigo", "El campo Municipio es obligatorio.");

                if (String.IsNullOrEmpty(dep_codigo))
                    ModelState.AddModelError("Anvi_UsuarioCrea", "El campo Departamento es obligatorio.");

                if (ModelState.IsValid)
                {
                    IEnumerable<object> Insert = null;
                    string ErrorMessage = "";

                    Insert = db.UDP_Adm_tbAnticipoViatico_Insert(EmployeeID,
                                                                tbAnticipoViatico.Anvi_JefeInmediato,
                                                                Function.DatetimeNow(),
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
                                                                Function.DatetimeNow());
                    foreach (UDP_Adm_tbAnticipoViatico_Insert_Result Res in Insert)
                        ErrorMessage = Res.MensajeError;

                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro contacte al administrador.");
                    }
                    else
                    {
                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage);
                        var EmpCreator = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select( x => new { x.emp_Id, nombre= x.emp_Nombres+" "+x.emp_Apellidos }).FirstOrDefault();
                        var EmpReciver = db.tbEmpleado.Where(x => x.emp_Id == tbAnticipoViatico.Anvi_JefeInmediato).Select( x => new { x.emp_Id, nombre= x.emp_Nombres+" "+x.emp_Apellidos,x.emp_CorreoElectronico }).FirstOrDefault();
                        Function.LeerDatosSol(out string pvMensajeError, EmpReciver.emp_CorreoElectronico, EmpReciver.nombre, EmpCreator.nombre);

                        if (!Result)
                            Function.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ErrorEmail);

                        return RedirectToAction("Index");
                    }
                        
                }
            }
            catch(Exception ex)
            {
                Function.BitacoraErrores("AnticipoViatico", "CreatePost", UserName, ex.Message.ToString());
            }
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
    }
}
