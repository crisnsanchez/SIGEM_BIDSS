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
    public class PuestoController : Controller
    {
        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();
        // GET: Puesto
        public IActionResult Index()
        {
            var sIGEM_BIDSSModel = db.TbPuesto.Include(t => t.Are);
            return View(sIGEM_BIDSSModel.ToList());
        }

        // GET: Puesto/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPuesto = db.TbPuesto
                .Include(t => t.Are)
                .FirstOrDefault(m => m.PtoId == id);
            if (tbPuesto == null)
            {
                return NotFound();
            }

            return View(tbPuesto);
        }

        // GET: Puesto/Create
        public IActionResult Create()
        {
            ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion");
            return View();
        }

        // POST: Puesto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PtoId,AreId,PtoDescripcion,PtoUsuarioCrea,PtoFechaCrea,PtoUsuarioModifica,PtoFechaModifica")] TbPuesto tbPuesto)
        {
            if (ModelState.IsValid)
            {
                db.Add(tbPuesto);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion", tbPuesto.AreId);
            return View(tbPuesto);
        }

        // GET: Puesto/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPuesto = db.TbPuesto.Find(id);
            if (tbPuesto == null)
            {
                return NotFound();
            }
            ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion", tbPuesto.AreId);
            return View(tbPuesto);
        }

        // POST: Puesto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PtoId,AreId,PtoDescripcion,PtoUsuarioCrea,PtoFechaCrea,PtoUsuarioModifica,PtoFechaModifica")] TbPuesto tbPuesto)
        {
            if (id != tbPuesto.PtoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(tbPuesto);
                    db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPuestoExists(tbPuesto.PtoId))
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
            ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion", tbPuesto.AreId);
            return View(tbPuesto);
        }

        // GET: Puesto/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPuesto = db.TbPuesto
                .Include(t => t.Are)
                .FirstOrDefault(m => m.PtoId == id);
            if (tbPuesto == null)
            {
                return NotFound();
            }

            return View(tbPuesto);
        }

        // POST: Puesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tbPuesto = db.TbPuesto.Find(id);
            db.TbPuesto.Remove(tbPuesto);
            db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbPuestoExists(int id)
        {
            return db.TbPuesto.Any(e => e.PtoId == id);
        }
    }
}
