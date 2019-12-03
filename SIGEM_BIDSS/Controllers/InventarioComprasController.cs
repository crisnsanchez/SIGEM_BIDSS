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
    public class InventarioComprasController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: InventarioCompras
        public ActionResult Index()
        {
            var tbInventarioCompra = db.tbInventarioCompra.Include(t => t.tbSolicitud);
            return View(tbInventarioCompra.ToList());
        }

        // GET: InventarioCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioCompra tbInventarioCompra = db.tbInventarioCompra.Find(id);
            if (tbInventarioCompra == null)
            {
                return HttpNotFound();
            }
            return View(tbInventarioCompra);
        }

        // GET: InventarioCompras/Create
        public ActionResult Create()
        {
            ViewBag.sol_Id = new SelectList(db.tbSolicitud, "sol_Id", "sol_GralDescripcion");
            return View();
        }

        // POST: InventarioCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invCom_Id,sol_Id,invCom_Descripcion,invCom_UsuarioCrea,invCom_FechaCrea,invCom_UsuarioModifica,invCom_FechaModifica")] tbInventarioCompra tbInventarioCompra)
        {
            if (ModelState.IsValid)
            {
                db.tbInventarioCompra.Add(tbInventarioCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sol_Id = new SelectList(db.tbSolicitud, "sol_Id", "sol_GralDescripcion", tbInventarioCompra.sol_Id);
            return View(tbInventarioCompra);
        }

        // GET: InventarioCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioCompra tbInventarioCompra = db.tbInventarioCompra.Find(id);
            if (tbInventarioCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.sol_Id = new SelectList(db.tbSolicitud, "sol_Id", "sol_GralDescripcion", tbInventarioCompra.sol_Id);
            return View(tbInventarioCompra);
        }

        // POST: InventarioCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invCom_Id,sol_Id,invCom_Descripcion,invCom_UsuarioCrea,invCom_FechaCrea,invCom_UsuarioModifica,invCom_FechaModifica")] tbInventarioCompra tbInventarioCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbInventarioCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sol_Id = new SelectList(db.tbSolicitud, "sol_Id", "sol_GralDescripcion", tbInventarioCompra.sol_Id);
            return View(tbInventarioCompra);
        }

        // GET: InventarioCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbInventarioCompra tbInventarioCompra = db.tbInventarioCompra.Find(id);
            if (tbInventarioCompra == null)
            {
                return HttpNotFound();
            }
            return View(tbInventarioCompra);
        }

        // POST: InventarioCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbInventarioCompra tbInventarioCompra = db.tbInventarioCompra.Find(id);
            db.tbInventarioCompra.Remove(tbInventarioCompra);
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
