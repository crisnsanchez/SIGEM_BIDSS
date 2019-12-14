﻿using System;
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
    public class ProveedorController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions _function = new GeneralFunctions();
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

        [HttpPost]
        public JsonResult GetMunicipios(string CodDepartamento)
        {
            var list = (from x in db.tbMunicipio where x.dep_codigo == CodDepartamento select new { mun_codigo = x.mun_codigo, mun_nombre = x.mun_nombre }).ToList();
            /*db.tbMunicipio.Where(x=> x.dep_codigo==CodDepartamento).ToList();*/
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: Proveedor/Create
        public ActionResult Create()
        {
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
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
                try
                {
                    ViewBag.selectedMun = tbProveedor.mun_codigo;
                    ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Inv_tbProveedor_Insert(tbProveedor.prov_Id,
                                                        tbProveedor.prov_Nombre,
                                                        tbProveedor.prov_NombreContacto,
                                                        tbProveedor.prov_Direccion,
                                                        tbProveedor.mun_codigo,
                                                        tbProveedor.prov_Email ,
                                                        tbProveedor.prov_Telefono,
                                                        tbProveedor.prov_RTN,
                                                        tbProveedor.acte_Id,
                                                        _function.GetUser(), _function.DatetimeNow());
                    foreach (UDP_Inv_tbProveedor_Insert_Result Permiso in List)
                        Msj = Permiso.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {

                        return View();
                    }
                    if (Msj.StartsWith("-2"))
                    {


                        ModelState.AddModelError("", "Ya existe un Permiso con el mismo nombre.");
                        return View(tbProveedor);
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
                    return View();
                }



            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
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
            ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
            ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
            ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
            return View(tbProveedor);
        }

        // POST: Proveedor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prov_Id,prov_Nombre,prov_NombreContacto,prov_Direccion,mun_codigo,prov_Email,prov_Telefono,prov_RTN,acte_Id,prov_UsuarioCrea,prov_FechaCrea,prov_UsuarioModifica,prov_FechaModifica")] tbProveedor tbProveedor)
        {
            try
            {

                ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
                ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
                ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                IEnumerable<Object> List = null;
                string Msj = "";
                List = db.UDP_Inv_tbProveedor_Update(tbProveedor.prov_Id,
                                                    tbProveedor.prov_Nombre,
                                                    tbProveedor.prov_NombreContacto,
                                                    tbProveedor.prov_Direccion,
                                                    tbProveedor.mun_codigo,
                                                    tbProveedor.prov_Email,
                                                    tbProveedor.prov_Telefono,
                                                    tbProveedor.prov_RTN,
                                                    tbProveedor.acte_Id,
                                                    _function.GetUser(), _function.DatetimeNow());
                foreach (UDP_Inv_tbProveedor_Update_Result Permiso in List)
                    Msj = Permiso.MensajeError;
                if (Msj.StartsWith("-1"))
                {
                    ViewBag.acte_Id = new SelectList(db.tbActividadEconomica, "acte_Id", "acte_Descripcion");
                    ViewBag.mun_codigo = new SelectList(db.tbMunicipio, "mun_codigo", "mun_nombre");
                    ViewBag.dep_Codigo = new SelectList(db.tbDepartamento, "dep_Codigo", "dep_Nombre");
                    return View();
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
                return View();
            }



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
