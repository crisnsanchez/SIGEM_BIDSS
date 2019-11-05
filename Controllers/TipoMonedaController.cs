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
        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();
        // GET: TipoMonedas
        public IActionResult Index()
        {
            return View(db.TbTipoMoneda.ToList());
        }

        // GET: TipoMonedas/Details/5
        public IActionResult Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoMoneda = db.TbTipoMoneda
                .FirstOrDefault(m => m.TmoId == id);
            if (tbTipoMoneda == null)
            {
                return NotFound();
            }

            return View(tbTipoMoneda);
        }

        // GET: TipoMonedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMonedas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TmoAbreviatura,TmoNombre,TmoUsuarioCrea")] TbTipoMoneda tbTipoMoneda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbTipoMoneda_Insert @p0, @p1, @p2", parameters: new object[] { tbTipoMoneda.TmoAbreviatura, tbTipoMoneda.TmoNombre, '1' });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbTipoMoneda);
                }

            }
            else
            {
                return View(tbTipoMoneda);
            }
        }

        // GET: TipoMonedas/Edit/5
        public IActionResult Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoMoneda = db.TbTipoMoneda.Find(id);
            if (tbTipoMoneda == null)
            {
                return NotFound();
            }
            return View(tbTipoMoneda);
        }

        // POST: TipoMonedas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, [Bind("TmoId,TmoAbreviatura,TmoNombre,TmoUsuarioModifica")] TbTipoMoneda tbTipoMoneda)
        {
            if (id != tbTipoMoneda.TmoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbTipoMoneda_Update @p0, @p1, @p2, @p3", parameters: new object[] { tbTipoMoneda.TmoId, tbTipoMoneda.TmoAbreviatura, tbTipoMoneda.TmoNombre, "1" });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbTipoMoneda);
                }

            }
            else
            {
                return View(tbTipoMoneda);
            }
        }

        // GET: TipoMonedas/Delete/5
        public IActionResult Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoMoneda = db.TbTipoMoneda
                .FirstOrDefault(m => m.TmoId == id);
            if (tbTipoMoneda == null)
            {
                return NotFound();
            }

            return View(tbTipoMoneda);
        }

        // POST: TipoMonedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            var tbTipoMoneda = db.TbTipoMoneda.Find(id);
            db.TbTipoMoneda.Remove(tbTipoMoneda);
            db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbTipoMonedaExists(short id)
        {
            return db.TbTipoMoneda.Any(e => e.TmoId == id);
        }
    }
}
