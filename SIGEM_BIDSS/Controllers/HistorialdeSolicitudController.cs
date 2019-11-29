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
    public class HistorialdeSolicitudController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: HistorialdeSolicitud
        public ActionResult Index()
        {
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion");
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
            ViewBag.tmo_id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura");
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion");
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            var tbHistorialdeSolicitud = db.tbHistorialdeSolicitud.Include(t => t.tbArea).Include(t => t.tbEmpleado).Include(t => t.tbMoneda).Include(t => t.tbPuesto).Include(t => t.tbTipoMovimiento).Include(t => t.tbTipoPermiso).Include(t => t.tbTipoSalario).Include(t => t.tbTipoSolicitud).Include(t => t.tbTipoViatico);
            return View(tbHistorialdeSolicitud.ToList());
        }

        // GET: HistorialdeSolicitud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialdeSolicitud tbHistorialdeSolicitud = db.tbHistorialdeSolicitud.Find(id);
            if (tbHistorialdeSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialdeSolicitud);
        }

        // GET: HistorialdeSolicitud/Create
        public ActionResult Create()
        {
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura");
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion");
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            return View();
        }

        // POST: HistorialdeSolicitud/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sol_Id,emp_Id,tipsol_Id,pto_Id,tpsal_id,tmo_Id,are_Id,tipmo_id,tpv_Id,tperm_Id,sol_GralDescripcion,sol_GralJefeInmediato,sol_GralCorreoJefeInmediato,sol_GralComentario,sol_GralJustificacion,sol_GralFechaSolicitud,sol_AnviFechaViaje,sol_Anvi_Cliente,sol_Anvi_LugarDestino,sol_Acper_Anterior,sol_Anvi_PropositoVisita,sol_Anvi_DiasVisita,sol_AnviHospedaje,sol_AnviTrasladoHacia,sol_AnsolMonto,sol_PerFechaRegreso,sol_PerMedioDia,sol_PerFechaInicio,sol_PerCantidadDias,sol_ReemMonto,sol_ReemFechaMonto,sol_ReemProveedor,sol_ReemCargoA,sol_ReemFechaGastos,sol_ReemNoFactura,sol_ReemMontoTotal,sol_AprRtn,sol_AprNombreEmpresa,sol_AprCiudad,sol_AprDireccion,sol_ApreTelefono,sol_ApreContactoAdm,sol_ApreCorreoAdm,sol_ApreNombreTecn,sol_ApreTelefonoTecn,sol_ApreCorreoTecn,sol_ApreCargoTecn,sol_ApreLink,sol_Acper_Nuevo,sol_RequeCantidad,sol_UsuarioCrea,sol_FechaCrea,sol_UsuarioModifica,sol_FechaModifica")] tbHistorialdeSolicitud tbHistorialdeSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.tbHistorialdeSolicitud.Add(tbHistorialdeSolicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbHistorialdeSolicitud.are_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbHistorialdeSolicitud.emp_Id);
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura", tbHistorialdeSolicitud.tmo_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbHistorialdeSolicitud.pto_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbHistorialdeSolicitud.tipmo_id);
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion", tbHistorialdeSolicitud.tperm_Id);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbHistorialdeSolicitud.tpsal_id);
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion", tbHistorialdeSolicitud.tipsol_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbHistorialdeSolicitud.tpv_Id);
            return View(tbHistorialdeSolicitud);
        }

        // GET: HistorialdeSolicitud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialdeSolicitud tbHistorialdeSolicitud = db.tbHistorialdeSolicitud.Find(id);
            if (tbHistorialdeSolicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbHistorialdeSolicitud.are_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbHistorialdeSolicitud.emp_Id);
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura", tbHistorialdeSolicitud.tmo_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbHistorialdeSolicitud.pto_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbHistorialdeSolicitud.tipmo_id);
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion", tbHistorialdeSolicitud.tperm_Id);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbHistorialdeSolicitud.tpsal_id);
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion", tbHistorialdeSolicitud.tipsol_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbHistorialdeSolicitud.tpv_Id);
            return View(tbHistorialdeSolicitud);
        }

        // POST: HistorialdeSolicitud/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sol_Id,emp_Id,tipsol_Id,pto_Id,tpsal_id,tmo_Id,are_Id,tipmo_id,tpv_Id,tperm_Id,sol_GralDescripcion,sol_GralJefeInmediato,sol_GralCorreoJefeInmediato,sol_GralComentario,sol_GralJustificacion,sol_GralFechaSolicitud,sol_AnviFechaViaje,sol_Anvi_Cliente,sol_Anvi_LugarDestino,sol_Acper_Anterior,sol_Anvi_PropositoVisita,sol_Anvi_DiasVisita,sol_AnviHospedaje,sol_AnviTrasladoHacia,sol_AnsolMonto,sol_PerFechaRegreso,sol_PerMedioDia,sol_PerFechaInicio,sol_PerCantidadDias,sol_ReemMonto,sol_ReemFechaMonto,sol_ReemProveedor,sol_ReemCargoA,sol_ReemFechaGastos,sol_ReemNoFactura,sol_ReemMontoTotal,sol_AprRtn,sol_AprNombreEmpresa,sol_AprCiudad,sol_AprDireccion,sol_ApreTelefono,sol_ApreContactoAdm,sol_ApreCorreoAdm,sol_ApreNombreTecn,sol_ApreTelefonoTecn,sol_ApreCorreoTecn,sol_ApreCargoTecn,sol_ApreLink,sol_Acper_Nuevo,sol_RequeCantidad,sol_UsuarioCrea,sol_FechaCrea,sol_UsuarioModifica,sol_FechaModifica")] tbHistorialdeSolicitud tbHistorialdeSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbHistorialdeSolicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbHistorialdeSolicitud.are_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbHistorialdeSolicitud.emp_Id);
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura", tbHistorialdeSolicitud.tmo_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbHistorialdeSolicitud.pto_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbHistorialdeSolicitud.tipmo_id);
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion", tbHistorialdeSolicitud.tperm_Id);
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion", tbHistorialdeSolicitud.tpsal_id);
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion", tbHistorialdeSolicitud.tipsol_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbHistorialdeSolicitud.tpv_Id);
            return View(tbHistorialdeSolicitud);
        }

        // GET: HistorialdeSolicitud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbHistorialdeSolicitud tbHistorialdeSolicitud = db.tbHistorialdeSolicitud.Find(id);
            if (tbHistorialdeSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tbHistorialdeSolicitud);
        }

        // POST: HistorialdeSolicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbHistorialdeSolicitud tbHistorialdeSolicitud = db.tbHistorialdeSolicitud.Find(id);
            db.tbHistorialdeSolicitud.Remove(tbHistorialdeSolicitud);
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
