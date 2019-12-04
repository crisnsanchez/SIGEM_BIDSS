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
            return View(db.tbInventarioCompra.ToList());
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
            return View();
        }

        // POST: InventarioCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "invCom_Id,invCom_Descripcion,invCom_UsuarioCrea,invCom_FechaCrea,invCom_UsuarioModifica,invCom_FechaModifica")] tbInventarioCompra tbInventarioCompra)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    _List = db.UDP_Gral_tbInventarioCompra_Insert(tbInventarioCompra.invCom_Descripcion, 1);
                    foreach (UDP_Gral_tbInventarioCompra_Insert_Result inv in _List)
                        MsjError = inv.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbInventarioCompra);
                    }
                    else
                    {
                        TempData["swalfunction"] = "true";

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbInventarioCompra);
                }

            }
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
            return View(tbInventarioCompra);
        }

        // POST: InventarioCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "invCom_Id,invCom_Descripcion,invCom_UsuarioCrea,invCom_FechaCrea,invCom_UsuarioModifica,invCom_FechaModifica")] tbInventarioCompra tbInventarioCompra)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    _List = db.UDP_Gral_tbInventarioCompra_Update(tbInventarioCompra.invCom_Id, tbInventarioCompra.invCom_Descripcion, 1);
                    foreach (UDP_Gral_tbInventarioCompra_Update_Result inv in _List)
                        MsjError = inv.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        return View(tbInventarioCompra);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbInventarioCompra);
                }
            }
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
