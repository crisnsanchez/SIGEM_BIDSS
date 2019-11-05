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
            ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion");
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
        public IActionResult Create([Bind("AreId,PtoDescripcion,PtoUsuarioCrea")] TbPuesto tbPuesto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _Insert = db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbPuesto_Insert @p0,@p1,@p2", parameters: new object[] { tbPuesto.AreId, tbPuesto.PtoDescripcion, "1" });
                    if (_Insert == 0)
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbPuesto);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    var Message = ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbPuesto);

                }
            }
            else
            {
                ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion", tbPuesto.AreId);
                return View(tbPuesto);
            }
        }
        // GET: Puesto/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPuesto = db.TbPuesto.Find(id);
            ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion", tbPuesto.AreId);
            if (tbPuesto == null)
            {
                return NotFound();
            }
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
                ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion", tbPuesto.AreId);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var _Insert = db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbPuesto_Update @p0, @p1, @p2,@p3",
                        parameters: new object[] { tbPuesto.PtoId, tbPuesto.AreId, tbPuesto.PtoDescripcion, 1 });
                    if (_Insert == 0)
                    {
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbPuesto);
                    }
                    else
                    {
                        
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                 

                    var Message = ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbPuesto);
                }

            }
            else
            {
                ViewData["AreId"] = new SelectList(db.TbArea, "AreId", "AreDescripcion", tbPuesto.AreId);
                return View(tbPuesto);
            }
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
