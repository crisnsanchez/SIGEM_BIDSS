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
    public class DepartamentoController : Controller
    {
        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();

        // GET: Departamento
        public IActionResult Index()
        {
            return View(db.TbDepartamento.ToList());
        }

        // GET: Departamento/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDepartamento = db.TbDepartamento
                .FirstOrDefault(m => m.DepId == id);
            if (tbDepartamento == null)
            {
                return NotFound();
            }

            return View(tbDepartamento);
        }

        // GET: Departamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DepId,DepDescripcion,DepUsuarioCrea,DepFechaCrea,DepUsuarioModifica,DepFechaModifica")] TbDepartamento tbDepartamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbDepartamento_Insert @p0, @p1, @p2",
                        parameters: new object[] { tbDepartamento.DepId,
                                                   tbDepartamento.DepDescripcion,
                                                   tbDepartamento.DepUsuarioCrea});
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbDepartamento);
                }

            }
            else
            {
                return View(tbDepartamento);
            }
        }

        // GET: Departamento/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDepartamento = db.TbDepartamento.Find(id);
            if (tbDepartamento == null)
            {
                return NotFound();
            }
            return View(tbDepartamento);
        }

        // POST: Departamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("DepId,DepDescripcion,DepUsuarioCrea,DepFechaCrea,DepUsuarioModifica,DepFechaModifica")] TbDepartamento tbDepartamento)
        {
            if (id != tbDepartamento.DepId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbDepartamento_Update @p0, @p1, @p2",
                        parameters: new object[] { tbDepartamento.DepId,
                                                   tbDepartamento.DepDescripcion,
                                                   tbDepartamento.DepUsuarioModifica});
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbDepartamento);
                }

            }
            else
            {
                return View(tbDepartamento);
            }
        }

        // GET: Departamento/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDepartamento = db.TbDepartamento
                .FirstOrDefault(m => m.DepId == id);
            if (tbDepartamento == null)
            {
                return NotFound();
            }

            return View(tbDepartamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var tbDepartamento = db.TbDepartamento.Find(id);
            db.TbDepartamento.Remove(tbDepartamento);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDepartamentoExists(string id)
        {
            return db.TbDepartamento.Any(e => e.DepId == id);
        }
    }
}
