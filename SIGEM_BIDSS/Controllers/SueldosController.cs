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
    public class SueldosController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Sueldos
        public ActionResult Index()
        {
            var tbSueldos = db.tbSueldos.Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbMoneda).Include(t => t.tbSueldos2).Include(t => t.tbSueldos3);
            return View(tbSueldos.ToList());
        }

        // GET: Sueldos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSueldos tbSueldos = db.tbSueldos.Find(id);
            if (tbSueldos == null)
            {
                return HttpNotFound();
            }
            return View(tbSueldos);
        }

        // GET: Sueldos/Create
        public ActionResult Create()
        {
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura");
            ViewBag.sue_SueldoAnterior = new SelectList(db.tbSueldos, "sue_Id", "sue_RazonInactivo");
            ViewBag.sue_SueldoAnterior = new SelectList(db.tbSueldos, "sue_Id", "sue_RazonInactivo");
            return View();
        }

        // POST: Sueldos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sue_Id,emp_Id,tmo_Id,sue_Cantidad,sue_SueldoAnterior,sue_Estado,sue_RazonInactivo,sue_UsuarioCrea,sue_FechaCrea,ue_UsuarioModifica,sue_FechaModifica")] tbSueldos tbSueldos)
        {
          
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Plani_tbSueldos_Insert(tbSueldos.emp_Id, tbSueldos.tmo_Id, tbSueldos.sue_Cantidad, tbSueldos.sue_SueldoAnterior, tbSueldos.sue_Estado, tbSueldos.sue_RazonInactivo, 1);
                    foreach (UDP_Plani_tbSueldos_Insert_Result sueldo in List)
                        Msj = sueldo.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        ViewBag.emp_id = new SelectList(db.tbEmpleado, "emp_Id", "emp_nombres", tbSueldos.emp_Id);
                        ViewBag.tmo_Id = new SelectList(db.tbEmpleado, "tmo_Id", "tmo_Abrievatura", tbSueldos.tmo_Id);
                        
                       
                        return View(tbSueldos);
                    }
                    else
                    {
                        TempData["swalfunction"] = "true";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador." + Ex.Message.ToString());
                    ViewBag.emp_id = new SelectList(db.tbEmpleado, "emp_Id", "emp_nombres", tbSueldos.emp_Id);
                    ViewBag.tmo_Id = new SelectList(db.tbEmpleado, "tmo_Id", "tmo_Abrievatura", tbSueldos.tmo_Id);

                    return View(tbSueldos);
                }
            else
            {
                ViewBag.emp_id = new SelectList(db.tbEmpleado, "emp_Id", "emp_nombres", tbSueldos.emp_Id);
                ViewBag.tmo_Id = new SelectList(db.tbEmpleado, "tmo_Id", "tmo_Abrievatura", tbSueldos.tmo_Id);

                return View(tbSueldos);
            }
    }

        // GET: Sueldos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSueldos tbSueldos = db.tbSueldos.Find(id);
            if (tbSueldos == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSueldos.emp_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSueldos.emp_Id);
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura", tbSueldos.tmo_Id);
            ViewBag.sue_SueldoAnterior = new SelectList(db.tbSueldos, "sue_Id", "sue_RazonInactivo", tbSueldos.sue_SueldoAnterior);
            ViewBag.sue_SueldoAnterior = new SelectList(db.tbSueldos, "sue_Id", "sue_RazonInactivo", tbSueldos.sue_SueldoAnterior);
            return View(tbSueldos);
        }

        // POST: Sueldos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sue_Id,emp_Id,tmo_Id,sue_Cantidad,sue_SueldoAnterior,sue_Estado,sue_RazonInactivo,sue_UsuarioCrea,sue_FechaCrea,ue_UsuarioModifica,sue_FechaModifica")] tbSueldos tbSueldos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSueldos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSueldos.emp_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSueldos.emp_Id);
            ViewBag.tmo_Id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura", tbSueldos.tmo_Id);
            ViewBag.sue_SueldoAnterior = new SelectList(db.tbSueldos, "sue_Id", "sue_RazonInactivo", tbSueldos.sue_SueldoAnterior);
            ViewBag.sue_SueldoAnterior = new SelectList(db.tbSueldos, "sue_Id", "sue_RazonInactivo", tbSueldos.sue_SueldoAnterior);
            return View(tbSueldos);
        }

        // GET: Sueldos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSueldos tbSueldos = db.tbSueldos.Find(id);
            if (tbSueldos == null)
            {
                return HttpNotFound();
            }
            return View(tbSueldos);
        }

        // POST: Sueldos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSueldos tbSueldos = db.tbSueldos.Find(id);
            db.tbSueldos.Remove(tbSueldos);
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
