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
    public class VacacionesPermisosEspecialesController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

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

        // GET: VacacionesPermisosEspeciales/Create
        public ActionResult Create()
        {
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.VPE_JefeInmediato = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion");
            return View();
        }

        // POST: VacacionesPermisosEspeciales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VPE_Id,VPE_Correlativo,emp_Id,VPE_JefeInmediato,tperm_Id,est_Id,VPE_GralFechaSolicitud,VPE_FechaInicio,VPE_FechaFin,VPE_CantidadDias,VPE_MontoSolicitado,VPE_Comentario,VPE_RazonRechazo,VPE_UsuarioCrea,VPE_FechaCrea,VPE_UsuarioModifica,VPE_FechaModifica")] tbVacacionesPermisosEspeciales tbVacacionesPermisosEspeciales)
        {
            if (ModelState.IsValid)
            {
                db.tbVacacionesPermisosEspeciales.Add(tbVacacionesPermisosEspeciales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
