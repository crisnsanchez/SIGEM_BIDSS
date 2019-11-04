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
        private readonly SIGEM_BIDSSModel _context;

        public PuestoController(SIGEM_BIDSSModel context)
        {
            _context = context;
        }

        // GET: Puesto
        public async Task<IActionResult> Index()
        {
            var sIGEM_BIDSSModel = _context.TbPuesto.Include(p => p.Are);
            return View(await sIGEM_BIDSSModel.ToListAsync());
        }

        // GET: Puesto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puesto = await _context.TbPuesto
                .Include(p => p.Are)
                .FirstOrDefaultAsync(m => m.PtoId == id);
            if (puesto == null)
            {
                return NotFound();
            }

            return View(puesto);
        }

        // GET: Puesto/Create
        public IActionResult Create()
        {
            ViewData["AreId"] = new SelectList(_context.TbArea, "AreId", "AreDescripcion");
            return View();
        }

        // POST: Puesto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PtoId,AreId,PtoDescripcion,PtoUsuarioCrea,PtoFechaCrea,PtoUsuarioModifica,PtoFechaModifica")] Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreId"] = new SelectList(_context.TbArea, "AreId", "AreDescripcion", puesto.AreId);
            return View(puesto);
        }

        // GET: Puesto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puesto = await _context.TbPuesto.FindAsync(id);
            if (puesto == null)
            {
                return NotFound();
            }
            ViewData["AreId"] = new SelectList(_context.TbArea, "AreId", "AreDescripcion", puesto.AreId);
            return View(puesto);
        }

        // POST: Puesto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PtoId,AreId,PtoDescripcion,PtoUsuarioCrea,PtoFechaCrea,PtoUsuarioModifica,PtoFechaModifica")] Puesto puesto)
        {
            if (id != puesto.PtoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuestoExists(puesto.PtoId))
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
            ViewData["AreId"] = new SelectList(_context.TbArea, "AreId", "AreDescripcion", puesto.AreId);
            return View(puesto);
        }

        // GET: Puesto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puesto = await _context.TbPuesto
                .Include(p => p.Are)
                .FirstOrDefaultAsync(m => m.PtoId == id);
            if (puesto == null)
            {
                return NotFound();
            }

            return View(puesto);
        }

        // POST: Puesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puesto = await _context.TbPuesto.FindAsync(id);
            _context.TbPuesto.Remove(puesto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuestoExists(int id)
        {
            return _context.TbPuesto.Any(e => e.PtoId == id);
        }
    }
}
