﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;
using System.Transactions;

namespace SIGEM_BIDSS.Controllers
{
    public class PuestoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Puesto
        public ActionResult Index()

        {
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            return View(db.tbPuesto.ToList());
        }

        // GET: Puesto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuesto tbPuesto = db.tbPuesto.Find(id);
            if (tbPuesto == null)
            {
                return HttpNotFound();
            }
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            return View(tbPuesto);
        }

        // GET: Puesto/Create
        public ActionResult Create()
        {
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            return View();
        }

        // POST: Puesto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pto_Id,are_Id,pto_Descripcion,pto_UsuarioCrea,pto_FechaCrea,pto_UsuarioModifica,pto_FechaModifica")] tbPuesto tbPuesto)
        {
            if (ModelState.IsValid)
                using (TransactionScope _Tran = new TransactionScope())
                {
                    try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbPuesto_Insert(tbPuesto.are_Id, tbPuesto.pto_Descripcion,1);
                    foreach (UDP_Gral_tbPuesto_Insert_Result TipoSangre in List)
                        Msj = TipoSangre.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View();
                }

                }

            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            return View(tbPuesto);
        }


        // GET: Puesto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuesto tbPuesto = db.tbPuesto.Find(id);
            if (tbPuesto == null)
            {
                return HttpNotFound();
            }
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            return View(tbPuesto);
        }

        // POST: Puesto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pto_Id,are_Id,pto_Descripcion,pto_UsuarioCrea,pto_FechaCrea,pto_UsuarioModifica,pto_FechaModifica")] tbPuesto tbPuesto)
        {
            if (ModelState.IsValid)
                try
                {
                    IEnumerable<Object> List = null;
                    string Msj = "";
                    List = db.UDP_Gral_tbPuesto_Update(tbPuesto.pto_Id,tbPuesto.are_Id, tbPuesto.pto_Descripcion, 1);
                    foreach (UDP_Gral_tbPuesto_Update_Result TipoSangre in List)
                        Msj = TipoSangre.MensajeError;
                    if (Msj.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View();
                }


            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            return View(tbPuesto);
        }

        // GET: Puesto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPuesto tbPuesto = db.tbPuesto.Find(id);
            if (tbPuesto == null)
            {
                return HttpNotFound();
            }
            return View(tbPuesto);
        }

        // POST: Puesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPuesto tbPuesto = db.tbPuesto.Find(id);
            db.tbPuesto.Remove(tbPuesto);
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