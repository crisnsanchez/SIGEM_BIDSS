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
        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();

        // GET: TipoSangre
        public IActionResult Index()
        {
            return View(db.TbTipoSangre.ToList());
        }

        // GET: TipoSangre/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSangre = db.TbTipoSangre
                .FirstOrDefault(m => m.TpsId == id);
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
        public IActionResult Create([Bind("TpsNombre,TpsUsuarioCrea")] TbTipoSangre tbTipoSangre)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbTipoSangre_Insert @p0, @p1", parameters: new object[] { tbTipoSangre.TpsNombre, '1' });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbTipoSangre);
                }

            }
            else
            {
                return View(tbTipoSangre);
            }
        }

        // GET: TipoSangre/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSangre = db.TbTipoSangre.Find(id);
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
        public IActionResult Edit(int id, [Bind("TpsId,TpsNombre,TpsUsuarioModifica")] TbTipoSangre tbTipoSangre)
        {
            if (id != tbTipoSangre.TpsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbTipoSangre_Update @p0, @p1, @p2", parameters: new object[] { tbTipoSangre.TpsId, tbTipoSangre.TpsNombre, "1" });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbTipoSangre);
                }

            }
            else
            {
                return View(tbTipoSangre);
            }
        }

        // GET: TipoSangre/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbTipoSangre = db.TbTipoSangre
                .FirstOrDefault(m => m.TpsId == id);
            if (tbTipoSangre == null)
            {
                return NotFound();
            }

            return View(tbTipoSangre);
        }

        // POST: TipoSangre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tbTipoSangre = db.TbTipoSangre.Find(id);
            db.TbTipoSangre.Remove(tbTipoSangre);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TbTipoSangreExists(int id)
        {
            return db.TbTipoSangre.Any(e => e.TpsId == id);
        }
    }
}
