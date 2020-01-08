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
    public class SolicitudReembolsoGastosDetalleController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: SolicitudReembolsoGastosDetalle
        public async Task<ActionResult> Index()
        {
            var tbSolicitudReembolsoGastosDetalle = db.tbSolicitudReembolsoGastosDetalle.Include(t => t.tbSolicitudReembolsoGastos).Include(t => t.tbTipoViatico);
            return View(await tbSolicitudReembolsoGastosDetalle.ToListAsync());
        }

        // GET: SolicitudReembolsoGastosDetalle/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = await db.tbSolicitudReembolsoGastosDetalle.FindAsync(id);
            if (tbSolicitudReembolsoGastosDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // GET: SolicitudReembolsoGastosDetalle/Create
        public ActionResult Create()
        {
            ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            return View();
        }

        // POST: SolicitudReembolsoGastosDetalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReemgaDet_Id,Reemga_Id,ReemgaDet_FechaGasto,tpv_Id,ReemgaDet_MontoGasto,ReemgaDet_Concepto,ReemgaDet_Archivo")] tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle)
        {
            if (ModelState.IsValid)
            {
                db.tbSolicitudReembolsoGastosDetalle.Add(tbSolicitudReembolsoGastosDetalle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo", tbSolicitudReembolsoGastosDetalle.Reemga_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitudReembolsoGastosDetalle.tpv_Id);
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // GET: SolicitudReembolsoGastosDetalle/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = await db.tbSolicitudReembolsoGastosDetalle.FindAsync(id);
            if (tbSolicitudReembolsoGastosDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo", tbSolicitudReembolsoGastosDetalle.Reemga_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitudReembolsoGastosDetalle.tpv_Id);
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // POST: SolicitudReembolsoGastosDetalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReemgaDet_Id,Reemga_Id,ReemgaDet_FechaGasto,tpv_Id,ReemgaDet_MontoGasto,ReemgaDet_Concepto,ReemgaDet_Archivo")] tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitudReembolsoGastosDetalle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo", tbSolicitudReembolsoGastosDetalle.Reemga_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbSolicitudReembolsoGastosDetalle.tpv_Id);
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // GET: SolicitudReembolsoGastosDetalle/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = await db.tbSolicitudReembolsoGastosDetalle.FindAsync(id);
            if (tbSolicitudReembolsoGastosDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastosDetalle);
        }

        // POST: SolicitudReembolsoGastosDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbSolicitudReembolsoGastosDetalle tbSolicitudReembolsoGastosDetalle = await db.tbSolicitudReembolsoGastosDetalle.FindAsync(id);
            db.tbSolicitudReembolsoGastosDetalle.Remove(tbSolicitudReembolsoGastosDetalle);
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
    }
}
