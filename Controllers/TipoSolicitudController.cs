﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class TipoSolicitudController : Controller
    {
        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();
        // GET: TipoSolicitud
        public IActionResult Index()
        {
            return View(db.TbTipoSolicitud.ToList());
        }

        // GET: TipoSolicitud/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = db.TbTipoSolicitud
                .FirstOrDefault(m => m.TipsolId == id);
            if (tbTipoSolicitud == null)
            {
                return NotFound();
            }

            return View(tbTipoSolicitud);
        }

        // GET: TipoSolicitud/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoSolicitud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TipsolDescripcion,TipsolUsuarioCrea")] TbTipoSolicitud tbTipoSolicitud)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbTipoSolicitud_Insert @p0, @p1", parameters: new object[] { tbTipoSolicitud.TipsolDescripcion, '1' });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbTipoSolicitud);
                }

            }
            else
            {
                return View(tbTipoSolicitud);
            }
        }

        // GET: TipoSolicitud/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = db.TbTipoSolicitud.Find(id);
            if (tbTipoSolicitud == null)
            {
                return NotFound();
            }
            return View(tbTipoSolicitud);
        }

        // POST: TipoSolicitud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("TipsolId,TipsolDescripcion,TipsolUsuarioModifica")] TbTipoSolicitud tbTipoSolicitud)
        {
            if (id != tbTipoSolicitud.TipsolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbTipoSolicitud_Update @p0, @p1, @p2", parameters: new object[] { tbTipoSolicitud.TipsolId, tbTipoSolicitud.TipsolDescripcion, "1" });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbTipoSolicitud);
                }

            }
            else
            {
                return View(tbTipoSolicitud);
            }
        }

        // GET: TipoSolicitud/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = db.TbTipoSolicitud
                .FirstOrDefault(m => m.TipsolId == id);
            if (tbTipoSolicitud == null)
            {
                return NotFound();
            }

            return View(tbTipoSolicitud);
        }

        // POST: TipoSolicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tbTipoSolicitud = db.TbTipoSolicitud.Find(id);
            db.TbTipoSolicitud.Remove(tbTipoSolicitud);
            db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTipoSolicitudExists(int id)
        {
            return db.TbTipoSolicitud.Any(e => e.TipsolId == id);
        }
    }
}
