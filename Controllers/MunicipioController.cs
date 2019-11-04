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
    public class MunicipioController : Controller
    {
        private readonly SIGEM_BIDSSModel _context;

        public MunicipioController(SIGEM_BIDSSModel context)
        {
            _context = context;
        }

        // GET: Municipios
        public async Task<IActionResult> Index()
        {
            var sIGEM_BIDSSModel = _context.TbMunicipio.Include(t => t.Dep);
            return View(await sIGEM_BIDSSModel.ToListAsync());
        }

        // GET: Municipios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMunicipio = await _context.TbMunicipio
                .Include(t => t.Dep)
                .FirstOrDefaultAsync(m => m.MunId == id);
            if (tbMunicipio == null)
            {
                return NotFound();
            }

            return View(tbMunicipio);
        }

        // GET: Municipios/Create
        public IActionResult Create()
        {
            ViewData["DepId"] = new SelectList(_context.TbDepartamento, "DepId", "DepId");
            return View();
        }

        // POST: Municipios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MunId,DepId,MunNombre,MunUsuarioCrea,MunFechaCrea,MunUsuarioModifica,MunFechaModifica")] TbMunicipio tbMunicipio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbMunicipio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepId"] = new SelectList(_context.TbDepartamento, "DepId", "DepId", tbMunicipio.DepId);
            return View(tbMunicipio);
        }

        // GET: Municipios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMunicipio = await _context.TbMunicipio.FindAsync(id);
            if (tbMunicipio == null)
            {
                return NotFound();
            }
            ViewData["DepId"] = new SelectList(_context.TbDepartamento, "DepId", "DepId", tbMunicipio.DepId);
            return View(tbMunicipio);
        }

        // POST: Municipios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MunId,DepId,MunNombre,MunUsuarioCrea,MunFechaCrea,MunUsuarioModifica,MunFechaModifica")] TbMunicipio tbMunicipio)
        {
            if (id != tbMunicipio.MunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbMunicipio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbMunicipioExists(tbMunicipio.MunId))
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
            ViewData["DepId"] = new SelectList(_context.TbDepartamento, "DepId", "DepId", tbMunicipio.DepId);
            return View(tbMunicipio);
        }

        // GET: Municipios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMunicipio = await _context.TbMunicipio
                .Include(t => t.Dep)
                .FirstOrDefaultAsync(m => m.MunId == id);
            if (tbMunicipio == null)
            {
                return NotFound();
            }

            return View(tbMunicipio);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tbMunicipio = await _context.TbMunicipio.FindAsync(id);
            _context.TbMunicipio.Remove(tbMunicipio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbMunicipioExists(string id)
        {
            return _context.TbMunicipio.Any(e => e.MunId == id);
        }
    }
}
