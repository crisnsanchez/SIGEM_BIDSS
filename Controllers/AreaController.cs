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
    public class AreaController : Controller
    {

        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();
       
        // GET: Area
        public IActionResult Index()
        {
            return View( db.TbArea);
        }

        // GET: Area/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbArea =db.TbArea
                .FirstOrDefault(m => m.AreId == id);
            if (tbArea == null)
            {
                return NotFound();
            }

            return View(tbArea);
        }

        // GET: Area/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Area/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AreId,AreDescripcion,AreUsuarioCrea,AreFechaCrea,AreUsuarioModifica,AreFechaModifica")] TbArea tbArea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbArea_Insert @p0, @p1", parameters: new object[] { tbArea.AreDescripcion, tbArea.AreUsuarioCrea });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbArea);
                }

            }
            else
            {
                return View(tbArea);
            }
        }

      
        // GET: Area/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbArea = db.TbArea.Find(id);
            if (tbArea == null)
            {
                return NotFound();
            }
            return View(tbArea);
        }

        // POST: Area/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AreId,AreDescripcion,AreUsuarioCrea,AreFechaCrea,AreUsuarioModifica,AreFechaModifica")] TbArea tbArea)
        {
            if (id != tbArea.AreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(tbArea);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbAreaExists(tbArea.AreId))
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
            return View(tbArea);
        }

        // GET: Area/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbArea = db.TbArea
                .FirstOrDefault(m => m.AreId == id);
            if (tbArea == null)
            {
                return NotFound();
            }

            return View(tbArea);
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tbArea = db.TbArea.Find(id);
            db.TbArea.Remove(tbArea);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TbAreaExists(int id)
        {
            return db.TbArea.Any(e => e.AreId == id);
        }
    }
}
