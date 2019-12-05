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
    [Authorize]
    public class AreaController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Area
        public ActionResult Index()
        {
            try
            {
                return View(db.tbArea.ToList());
            }
            catch (Exception Ex)
            {
                //throw;
                return RedirectToAction("Error500", "Home");
            }
        }

        // GET: Area/Details/5
        public ActionResult Details(int? id)
        {
            try
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbArea tbArea = db.tbArea.Find(id);
                if (tbArea == null)
                {
                    return HttpNotFound();
                }
                return View(tbArea);
            }
            catch (Exception Ex)
            {      //throw;
                return RedirectToAction("Error500", "Home");
            }
        }
            // GET: Area/Create
            public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }
        // POST: Area/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "are_Id,are_Descripcion,are_UsuarioCrea")] tbArea tbArea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    _List = db.UDP_Gral_tbArea_Insert(tbArea.are_Descripcion,1, String.Format("{0:HH:mm:ss}", DateTime.Now));
                    foreach (UDP_Gral_tbArea_Insert_Result Area in _List)
                        MsjError = Area.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbArea);
                    }
                    if (MsjError.StartsWith("-2"))
                    {
                       
                        ModelState.AddModelError("", "Ya existe un Área con el mismo nombre.");
                        return View(tbArea);
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
                    return View(tbArea);
                }

            }
            return View(tbArea);
        }

        // GET: Area/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tbArea tbArea = db.tbArea.Find(id);
                if (tbArea == null)
                {
                    return HttpNotFound();
                }
                return View(tbArea);

            }
            catch (Exception Ex)
            {
                return RedirectToAction("Error500", "Home");
            }
        }

        // POST: Area/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "are_Id,are_Descripcion,are_UsuarioCrea,are_FechaCrea,are_UsuarioModifica,are_FechaModifica")] tbArea tbArea)
        {
            if (ModelState.IsValid)
            {
                if (db.tbArea.Any(a =>  a.are_Descripcion == tbArea.are_Descripcion && a.are_Id != tbArea.are_Id))
                {
                    ModelState.AddModelError("", "Ya existe un Área con el mismo nombre.");
                }


                try
                {
                    IEnumerable<object> _List = null;
                    string MsjError = "";
                    _List = db.UDP_Gral_tbArea_Update(tbArea.are_Id,tbArea.are_Descripcion, 1, String.Format("{0:HH:mm:ss}", DateTime.Now));
                    foreach (UDP_Gral_tbArea_Update_Result _Area in _List)
                        MsjError = _Area.MensajeError;
                    if (MsjError.StartsWith("-1"))
                    {
                        return View(tbArea);
                    }
                    if (MsjError.StartsWith("-2"))
                    {

                        ModelState.AddModelError("", "Ya existe un estado con el mismo nombre.");
                        return View(tbArea);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbArea);
                }
            }
            return View(tbArea);
        }

        // GET: Area/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbArea tbArea = db.tbArea.Find(id);
            if (tbArea == null)
            {
                return HttpNotFound();
            }
            return View(tbArea);
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbArea tbArea = db.tbArea.Find(id);
            db.tbArea.Remove(tbArea);
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
