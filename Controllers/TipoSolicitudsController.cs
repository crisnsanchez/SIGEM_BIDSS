using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class TipoSolicitudsController : Controller
    {

        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();
       

     
        // GET: TipoSolicituds
        public IActionResult Index()
        {
            return View(db.TbTipoSolicitud);
        }

        // GET: TipoSolicituds/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = db.TbTipoSolicitud
                .FirstOrDefaultAsync(m => m.TipsolId == id);
            if (tbTipoSolicitud == null)
            {
                return NotFound();
            }

            return View(tbTipoSolicitud);
        }

        // GET: TipoSolicituds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoSolicituds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TipsolId,TipsolDescripcion,TipsolUsuarioCrea,TipsolFechaCrea,TipsolUsuarioModifica,TipsolFechaModifica")] TbTipoSolicitud tbTipoSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Add(tbTipoSolicitud);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tbTipoSolicitud);
        }

        // GET: TipoSolicituds/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = db.TbTipoSolicitud.FindAsync(id);
            if (tbTipoSolicitud == null)
            {
                return NotFound();
            }
            return View(tbTipoSolicitud);
        }

        // POST: TipoSolicituds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("TipsolId,TipsolDescripcion,TipsolUsuarioCrea,TipsolFechaCrea,TipsolUsuarioModifica,TipsolFechaModifica")] TbTipoSolicitud tbTipoSolicitud)
        {
            if (id != tbTipoSolicitud.TipsolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(tbTipoSolicitud);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbTipoSolicitudExists(tbTipoSolicitud.TipsolId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbTipoSolicitud);
        }

        // GET: TipoSolicituds/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = db.TbTipoSolicitud
                .FirstOrDefaultAsync(m => m.TipsolId == id);
            if (tbTipoSolicitud == null)
            {
                return NotFound();
            }

            return View(tbTipoSolicitud);
        }

        // POST: TipoSolicituds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tbTipoSolicitud = db.TbTipoSolicitud.Find(id);
            db.TbTipoSolicitud.Remove(tbTipoSolicitud);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTipoSolicitudExists(int id)
        {
            return db.TbTipoSolicitud.Any(e => e.TipsolId == id);
        }
    }
}
