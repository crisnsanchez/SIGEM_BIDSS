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
    public class SolicitudController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Solicitud
        public ActionResult Index()
        {
            var tbSolicitud = db.tbSolicitud.Include(t => t.tbEmpleado).Include(t => t.tbPuesto).Include(t => t.tbTipoMovimiento).Include(t => t.tbTipoSolicitud).Include(t => t.tbTipoMoneda).Include(t => t.tbTipoSalario).Include(t => t.tbTipoViatico);
            return View(tbSolicitud.ToList());
        }

        // GET: Solicitud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitud tbSolicitud = db.tbSolicitud.Find(id);
            if (tbSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitud);
        }

        // GET: Solicitud/Create
        public ActionResult Create()
        {
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion");
            ViewBag.tmo_id = new SelectList(db.tbTipoMoneda, "tmo_Id", "tmo_Abreviatura");
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            return View();
        }

        // POST: Solicitud/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sol_Id,sol_Descripcion,emp_Id,tipsol_Id,pto_Id,sol_JefeInmediato,sol_CorreoJefeInmediato,sol_PropositoVisita,sol_LugarDestino,sol_TrasladoHacia,sol_Hospedaje,tpsal_id,sol_MontoSolicitud,tmo_id,are_Id,tipmo_id,tpv_Id,sol_Justificacion,sol_Cliente,sol_FechaSolicitud,sol_FechaRegreso,sol_FechaMonto,sol_CargoA,sol_NoFactura,sol_CantidadDias,sol_UsuarioCrea,sol_FechaCrea,sol_UsuarioModifica,sol_FechaModifica")] tbSolicitud tbSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.tbSolicitud.Add(tbSolicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSolicitud.emp_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbSolicitud.pto_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbSolicitud.tipmo_id);
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion", tbSolicitud.tipsol_Id);
            ViewBag.tmo_id = new SelectList(db.tbTipoMoneda, "tmo_Id", "tmo_Abreviatura", tbSolicitud.tmo_id);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbSolicitud.tpsal_id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitud.tpv_Id);
            return View(tbSolicitud);
        }

        // GET: Solicitud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitud tbSolicitud = db.tbSolicitud.Find(id);
            if (tbSolicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSolicitud.emp_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbSolicitud.pto_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbSolicitud.tipmo_id);
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion", tbSolicitud.tipsol_Id);
            ViewBag.tmo_id = new SelectList(db.tbTipoMoneda, "tmo_Id", "tmo_Abreviatura", tbSolicitud.tmo_id);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbSolicitud.tpsal_id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitud.tpv_Id);
            return View(tbSolicitud);
        }

        // POST: Solicitud/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sol_Id,sol_Descripcion,emp_Id,tipsol_Id,pto_Id,sol_JefeInmediato,sol_CorreoJefeInmediato,sol_PropositoVisita,sol_LugarDestino,sol_TrasladoHacia,sol_Hospedaje,tpsal_id,sol_MontoSolicitud,tmo_id,are_Id,tipmo_id,tpv_Id,sol_Justificacion,sol_Cliente,sol_FechaSolicitud,sol_FechaRegreso,sol_FechaMonto,sol_CargoA,sol_NoFactura,sol_CantidadDias,sol_UsuarioCrea,sol_FechaCrea,sol_UsuarioModifica,sol_FechaModifica")] tbSolicitud tbSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSolicitud.emp_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbSolicitud.pto_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbSolicitud.tipmo_id);
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion", tbSolicitud.tipsol_Id);
            ViewBag.tmo_id = new SelectList(db.tbTipoMoneda, "tmo_Id", "tmo_Abreviatura", tbSolicitud.tmo_id);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbSolicitud.tpsal_id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitud.tpv_Id);
            return View(tbSolicitud);
        }

        // GET: Solicitud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitud tbSolicitud = db.tbSolicitud.Find(id);
            if (tbSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitud);
        }

        // POST: Solicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitud tbSolicitud = db.tbSolicitud.Find(id);
            db.tbSolicitud.Remove(tbSolicitud);
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
