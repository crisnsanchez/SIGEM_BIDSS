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
    public class AccionPersonalController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: AccionPersonal
        public async Task<ActionResult> Index()
        {
            var tbAccionPersonal = db.tbAccionPersonal.Include(t => t.tbEstado).Include(t => t.tbEmpleado).Include(t => t.tbTipoMovimiento).Include(t => t.tbEmpleado1).Include(t => t.tbEmpleado2).Include(t => t.tbEmpleado3);
            return View(await tbAccionPersonal.ToListAsync());
        }

        // GET: AccionPersonal/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: AccionPersonal/Create
        public ActionResult Create()
        {
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            return View();
        }

        // POST: AccionPersonal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Acp_Id,Acp_Correlativo,emp_Id,Acp_JefeInmediato,Acp_FechaSolicitud,tipmo_id,Acp_Comentario,est_Id,Acp_RazonRechazo,Acp_UsuarioCrea,Acp_FechaCrea,Acp_UsuarioModifica,Acp_FechaModifica")] tbAccionPersonal tbAccionPersonal)
        {
            if (ModelState.IsValid)
            {
                db.tbAccionPersonal.Add(tbAccionPersonal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbAccionPersonal.est_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbAccionPersonal.tipmo_id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
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
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbAccionPersonal.tipmo_id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
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
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento", tbAccionPersonal.tipmo_id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbAccionPersonal.emp_Id);
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
    }
}
