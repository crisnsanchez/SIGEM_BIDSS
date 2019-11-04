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
    public class TipoMonedaController : Controller
    {
        private readonly SIGEM_BIDSSModel _context;

        public TipoMonedaController(SIGEM_BIDSSModel context)
        {
            _context = context;
        }

        // GET: TipoMoneda
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbTipoMoneda.ToListAsync());
        }

        // GET: TipoMoneda/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMoneda = await _context.TbTipoMoneda
                .FirstOrDefaultAsync(m => m.TmoId == id);
            if (tipoMoneda == null)
            {
                return NotFound();
            }

            return View(tipoMoneda);
        }

        // GET: TipoMoneda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMoneda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TmoId,TmoAbreviatura,TmoNombre,TmoUsuarioCrea,TmoFechaCrea,TmoUsuarioModifica,TmoFechaModifica")] TipoMoneda tipoMoneda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoMoneda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMoneda);
        }

        // GET: TipoMoneda/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMoneda = await _context.TbTipoMoneda.FindAsync(id);
            if (tipoMoneda == null)
            {
                return NotFound();
            }
            return View(tipoMoneda);
        }

        // POST: TipoMoneda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("TmoId,TmoAbreviatura,TmoNombre,TmoUsuarioCrea,TmoFechaCrea,TmoUsuarioModifica,TmoFechaModifica")] TipoMoneda tipoMoneda)
        {
            if (id != tipoMoneda.TmoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMoneda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMonedaExists(tipoMoneda.TmoId))
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
            return View(tipoMoneda);
        }

        // GET: TipoMoneda/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMoneda = await _context.TbTipoMoneda
                .FirstOrDefaultAsync(m => m.TmoId == id);
            if (tipoMoneda == null)
            {
                return NotFound();
            }

            return View(tipoMoneda);
        }

        // POST: TipoMoneda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var tipoMoneda = await _context.TbTipoMoneda.FindAsync(id);
            _context.TbTipoMoneda.Remove(tipoMoneda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoMonedaExists(short id)
        {
            return _context.TbTipoMoneda.Any(e => e.TmoId == id);
        }
    }
}
