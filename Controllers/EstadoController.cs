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
        public  IActionResult Index()
        {
            return View( db.TbEstado.ToList());
        }

        // GET: Estado/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado =  db.TbEstado
                .FirstOrDefault(m => m.EstId == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
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
        public  IActionResult Create([Bind("EstId,EstDescripcion,EstUsuarioCrea,EstFechaCrea,EstUsuarioModifica,EstFechaModifica")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbEstado_Insert @p0, @p1", parameters: new object[] { estado.EstDescripcion, estado.EstUsuarioCrea });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(estado);
                }

            }
            else
            {
                return View(estado);
            }
        }

        // GET: Estado/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado =  db.TbEstado.Find(id);
            if (estado == null)
            {
                return NotFound();
            }
            return View(estado);
        }

        // POST: Estado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("EstId,EstDescripcion,EstUsuarioCrea,EstFechaCrea,EstUsuarioModifica,EstFechaModifica")] Estado estado)
        {
            if (id != estado.EstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(estado);
                     db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.EstId))
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
            return View(estado);
        }

        // GET: Estado/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado =  db.TbEstado
                .FirstOrDefault(m => m.EstId == id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            var estado =  db.TbEstado.Find(id);
            db.TbEstado.Remove(estado);
             db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoExists(int id)
        {
            return db.TbEstado.Any(e => e.EstId == id);
        }
    }
}
