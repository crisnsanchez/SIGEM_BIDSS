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
    public class TipoSangreController : Controller
    {
        private readonly SIGEM_BIDSSModel _context;

        public TipoSangreController(SIGEM_BIDSSModel context)
        {
            _context = context;
        }

        // GET: TipoSangre
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbTipoSangre.ToListAsync());
        }

        // GET: TipoSangre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSangre = await _context.TbTipoSangre
                .FirstOrDefaultAsync(m => m.TpsId == id);
            if (tbTipoSangre == null)
            {
                return NotFound();
            }

            return View(tbTipoSangre);
        }

        // GET: TipoSangre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoSangre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TpsId,TpsNombre,TpsUsuarioCrea,TpsFechaCrea,TpsUsuarioModifica,TpsFechaModifica")] TbTipoSangre tbTipoSangre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbTipoSangre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbTipoSangre);
        }

        // GET: TipoSangre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSangre = await _context.TbTipoSangre.FindAsync(id);
            if (tbTipoSangre == null)
            {
                return NotFound();
            }
            return View(tbTipoSangre);
        }

        // POST: TipoSangre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TpsId,TpsNombre,TpsUsuarioCrea,TpsFechaCrea,TpsUsuarioModifica,TpsFechaModifica")] TbTipoSangre tbTipoSangre)
        {
            if (id != tbTipoSangre.TpsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbTipoSangre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbTipoSangreExists(tbTipoSangre.TpsId))
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
            return View(tbTipoSangre);
        }

        // GET: TipoSangre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSangre = await _context.TbTipoSangre
                .FirstOrDefaultAsync(m => m.TpsId == id);
            if (tbTipoSangre == null)
            {
                return NotFound();
            }

            return View(tbTipoSangre);
        }

        // POST: TipoSangre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbTipoSangre = await _context.TbTipoSangre.FindAsync(id);
            _context.TbTipoSangre.Remove(tbTipoSangre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTipoSangreExists(int id)
        {
            return _context.TbTipoSangre.Any(e => e.TpsId == id);
        }
    }
}
