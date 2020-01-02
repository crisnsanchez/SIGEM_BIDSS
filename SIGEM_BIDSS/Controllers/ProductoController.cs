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
    public class ProductoController  : BaseController
        
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        GeneralFunctions Function = new GeneralFunctions();
        // GET: Producto
        public ActionResult Index()
        {
            var tbProducto = db.tbProducto.Include(t => t.tbUnidadMedida).Include(t => t.tbProductoSubcategoria).Include(t => t.tbProveedor);
            return View(tbProducto.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            return View(tbProducto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
            return View();
        }

        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "prod_Id,prod_Codigo,prod_CodigoBarras,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prov_Id,prod_EsActivo,prod_RazonInactivacion,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica")] tbProducto tbProducto)
        {
            if (db.tbProducto.Any(a => a.prod_CodigoBarras == tbProducto.prod_CodigoBarras))
            {
                ModelState.AddModelError("", "El Codigo de Barras ya Existe.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    
                    string MsjError = "";
                    IEnumerable<object> List = null;
                    List = db.UDP_Inv_tbProducto_Insert(
                                                        tbProducto.prod_Codigo,
                                                        tbProducto.prod_CodigoBarras,
                                                        tbProducto.prod_Descripcion,
                                                        tbProducto.prod_Marca,
                                                        tbProducto.prod_Modelo,
                                                        tbProducto.prod_Talla,
                                                        tbProducto.prod_Color,
                                                        tbProducto.pscat_Id,
                                                        tbProducto.uni_Id,
                                                        tbProducto.prov_Id,
                                                        tbProducto.prod_EsActivo,
                                                        tbProducto.prod_RazonInactivacion,
                                                        Function.GetUser(),
                                                        Function.DatetimeNow()
                                                            );
                    foreach (UDP_Inv_tbProducto_Insert_Result Producto in List)
                        MsjError = Producto.MensajeError;

                    if (MsjError.StartsWith("-1"))
                    {

                        ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
                        ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
                        ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                        ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
                        
                        ModelState.AddModelError("", "No se Pudo Insertar el Registro, Favor Contacte al Administrador.");
                        return View(tbProducto);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                   
                    ModelState.AddModelError("", "No se Pudo Insertar el Registro, Favor Contacte al Administrador.");
                    ViewBag.pcat_Id = new SelectList(db.tbProductoCategoria, "pcat_Id", "pcat_Nombre");
                    ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion");
                    ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion");
                    ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre");
                    return View(tbProducto);
                }
            }

            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
            return View(tbProducto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
            return View(tbProducto);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prod_Id,prod_Codigo,prod_CodigoBarras,prod_Descripcion,prod_Marca,prod_Modelo,prod_Talla,prod_Color,pscat_Id,uni_Id,prov_Id,prod_EsActivo,prod_RazonInactivacion,prod_UsuarioCrea,prod_FechaCrea,prod_UsuarioModifica,prod_FechaModifica")] tbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.uni_Id = new SelectList(db.tbUnidadMedida, "uni_Id", "uni_Descripcion", tbProducto.uni_Id);
            ViewBag.pscat_Id = new SelectList(db.tbProductoSubcategoria, "pscat_Id", "pscat_Descripcion", tbProducto.pscat_Id);
            ViewBag.prov_Id = new SelectList(db.tbProveedor, "prov_Id", "prov_Nombre", tbProducto.prov_Id);
            return View(tbProducto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbProducto tbProducto = db.tbProducto.Find(id);
            if (tbProducto == null)
            {
                return HttpNotFound();
            }
            return View(tbProducto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbProducto tbProducto = db.tbProducto.Find(id);
            db.tbProducto.Remove(tbProducto);
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
