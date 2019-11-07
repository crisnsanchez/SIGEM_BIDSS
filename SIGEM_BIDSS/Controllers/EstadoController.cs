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
    public class EstadoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Estado
        public ActionResult Index()
        {
            return View(db.tbEstado.ToList());
        }

        // GET: Estado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstado tbEstado = db.tbEstado.Find(id);
            if (tbEstado == null)
            {
                return HttpNotFound();
            }
            return View(tbEstado);
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "est_Id,est_Descripcion,est_UsuarioCrea,est_FechaCrea,est_UsuarioModifica,est_FechaModifica")] tbEstado tbEstado)
        {
            if (ModelState.IsValid)
            {
                db.tbEstado.Add(tbEstado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbEstado);
        }

        // GET: Estado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstado tbEstado = db.tbEstado.Find(id);
            if (tbEstado == null)
            {
                return HttpNotFound();
            }
            return View(tbEstado);
        }

        // POST: Estado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "est_Id,est_Descripcion,est_UsuarioCrea,est_FechaCrea,est_UsuarioModifica,est_FechaModifica")] tbEstado tbEstado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEstado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbEstado);
        }

        // GET: Estado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEstado tbEstado = db.tbEstado.Find(id);
            if (tbEstado == null)
            {
                return HttpNotFound();
            }
            return View(tbEstado);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbEstado tbEstado = db.tbEstado.Find(id);
            db.tbEstado.Remove(tbEstado);
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
