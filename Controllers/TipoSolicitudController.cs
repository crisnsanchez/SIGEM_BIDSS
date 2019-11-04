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
    public class TipoSolicitudController : Controller
    {
        private readonly SIGEM_BIDSSModel _context;

        public TipoSolicitudController(SIGEM_BIDSSModel context)
        {
            _context = context;
        }

        // GET: TipoSolicitud
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbTipoSolicitud.ToListAsync());
        }

        // GET: TipoSolicitud/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = await _context.TbTipoSolicitud
                .FirstOrDefaultAsync(m => m.TipsolId == id);
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
        public async Task<IActionResult> Create([Bind("TipsolId,TipsolDescripcion,TipsolUsuarioCrea,TipsolFechaCrea,TipsolUsuarioModifica,TipsolFechaModifica")] TipoSolicitud tbTipoSolicitud)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbTipoSolicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbTipoSolicitud);
        }

        // GET: TipoSolicitud/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = await _context.TbTipoSolicitud.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("TipsolId,TipsolDescripcion,TipsolUsuarioCrea,TipsolFechaCrea,TipsolUsuarioModifica,TipsolFechaModifica")] TipoSolicitud tbTipoSolicitud)
        {
            if (id != tbTipoSolicitud.TipsolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbTipoSolicitud);
                    await _context.SaveChangesAsync();
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

        // GET: TipoSolicitud/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSolicitud = await _context.TbTipoSolicitud
                .FirstOrDefaultAsync(m => m.TipsolId == id);
            if (tbTipoSolicitud == null)
            {
                return NotFound();
            }

            return View(tbTipoSolicitud);
        }

        // POST: TipoSolicitud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbTipoSolicitud = await _context.TbTipoSolicitud.FindAsync(id);
            _context.TbTipoSolicitud.Remove(tbTipoSolicitud);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTipoSolicitudExists(int id)
        {
            return _context.TbTipoSolicitud.Any(e => e.TipsolId == id);
        }
    }
}
