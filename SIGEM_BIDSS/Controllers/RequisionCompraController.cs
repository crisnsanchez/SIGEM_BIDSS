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
    public class RequisionCompraController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: RequisionCompra
        public async Task<ActionResult> Index()
        {
            var tbRequisionCompra = db.tbRequisionCompra.Include(t => t.tbArea).Include(t => t.tbEmpleado).Include(t => t.tbEstado);
            return View(await tbRequisionCompra.ToListAsync());
        }

        // GET: RequisionCompra/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            if (tbRequisionCompra == null)
            {
                return HttpNotFound();
            }
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
        public async Task<ActionResult> Create([Bind(Include = "Reqco_Id,Reqco_Correlativo,emp_Id,are_Id,Reqco_GralFechaSolicitud,Reqco_Comentario,est_Id,Reqco_RazonRechazo,Reqco_UsuarioCrea,Reqco_FechaCrea,Reqco_UsuarioModifica,Reqco_FechaModifica")] tbRequisionCompra tbRequisionCompra)
        {
            if (ModelState.IsValid)
            {
                db.tbRequisionCompra.Add(tbRequisionCompra);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbRequisionCompra.are_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbRequisionCompra.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbRequisionCompra.est_Id);
            return View(tbRequisionCompra);
        }

        // GET: RequisionCompra/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            if (tbRequisionCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbRequisionCompra.are_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbRequisionCompra.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbRequisionCompra.est_Id);
            return View(tbRequisionCompra);
        }

        // POST: RequisionCompra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Reqco_Id,Reqco_Correlativo,emp_Id,are_Id,Reqco_GralFechaSolicitud,Reqco_Comentario,est_Id,Reqco_RazonRechazo,Reqco_UsuarioCrea,Reqco_FechaCrea,Reqco_UsuarioModifica,Reqco_FechaModifica")] tbRequisionCompra tbRequisionCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbRequisionCompra).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion", tbRequisionCompra.are_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbRequisionCompra.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbRequisionCompra.est_Id);
            return View(tbRequisionCompra);
        }

        // GET: RequisionCompra/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            if (tbRequisionCompra == null)
            {
                return HttpNotFound();
            }
            return View(tbRequisionCompra);
        }

        // POST: RequisionCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tbRequisionCompra tbRequisionCompra = await db.tbRequisionCompra.FindAsync(id);
            db.tbRequisionCompra.Remove(tbRequisionCompra);
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
