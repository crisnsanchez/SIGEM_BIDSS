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
    public class EstadoController : Controller
    {
        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();

        // GET: Estado
        public IActionResult Index()
        {
            return View(db.TbEstado.ToList());
        }

        // GET: Estado/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = db.TbEstado
                .FirstOrDefault(m => m.EstId == id);
            if (tbEstado == null)
            {
                return NotFound();
            }

            return View(tbEstado);
        }

        // GET: Estado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EstId,EstDescripcion,EstUsuarioCrea,EstFechaCrea,EstUsuarioModifica,EstFechaModifica")] TbEstado tbEstado)
        {
            if (ModelState.IsValid)
            {
                db.Add(tbEstado);
                db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbEstado);
        }

        // GET: Estado/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = db.TbEstado.Find(id);
            if (tbEstado == null)
            {
                return NotFound();
            }
            return View(tbEstado);
        }

        // POST: Estado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EstId,EstDescripcion,EstUsuarioCrea,EstFechaCrea,EstUsuarioModifica,EstFechaModifica")] TbEstado tbEstado)
        {
            if (id != tbEstado.EstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(tbEstado);
                    db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEstadoExists(tbEstado.EstId))
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
            return View(tbEstado);
        }

        // GET: Estado/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = db.TbEstado
                .FirstOrDefault(m => m.EstId == id);
            if (tbEstado == null)
            {
                return NotFound();
            }

            return View(tbEstado);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tbEstado = db.TbEstado.Find(id);
            db.TbEstado.Remove(tbEstado);
            db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbEstadoExists(int id)
        {
            return db.TbEstado.Any(e => e.EstId == id);
        }
    }
}
