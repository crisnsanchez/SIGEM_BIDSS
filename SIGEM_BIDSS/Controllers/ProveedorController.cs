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
    public class ProveedorController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Proveedor
        public ActionResult Index()
        {
            var tbProveedor = db.tbProveedor.Include(t => t.tbActividadEconomica).Include(t => t.tbMunicipio);
            return View(tbProveedor.ToList());
        }

        // GET: Proveedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        // GET: Proveedor/Create
        public ActionResult Create()
        {
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo");
            return View();
        }

        // POST: Proveedor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prov_Id,prov_Nombre,prov_NombreContacto,prov_Direccion,mun_codigo,prov_Email,prov_Telefono,prov_RTN,acte_Id,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor)
        {
            if (ModelState.IsValid)
            {
                db.tbProveedor.Add(tbProveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbProveedor.mun_codigo);
            return View(tbProveedor);
        }

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbProveedor.mun_codigo);
            return View(tbProveedor);
        }

        // POST: Proveedor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prov_Id,prov_Nombre,prov_NombreContacto,prov_Direccion,mun_codigo,prov_Email,prov_Telefono,prov_RTN,acte_Id,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion", tbProveedor.acte_Id);
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbProveedor.mun_codigo);
            return View(tbProveedor);
        }

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            if (tbProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tbProveedor);
        }

        // POST: Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProveedor tbProveedor = db.tbProveedor.Find(id);
            db.tbProveedor.Remove(tbProveedor);
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
