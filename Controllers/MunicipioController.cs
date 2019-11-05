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
        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();


        // GET: Municipios
        public  IActionResult Index()
        {
           
            var sIGEM_BIDSSModel = db.TbMunicipio.Include(t => t.Dep);
            ViewData["DepId"] = new SelectList(db.TbDepartamento, "DepId", "DepDescripcion");
            return View(sIGEM_BIDSSModel);
        }

        // GET: Municipios/Details/5
        public  IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMunicipio =  db.TbMunicipio
                .Include(t => t.Dep)
                .FirstOrDefault(m => m.MunId == id);
            if (tbMunicipio == null)
            {
                return NotFound();
            }

            return View(tbMunicipio);
        }

        // GET: Municipios/Create
        public IActionResult Create()
        {
            ViewData["DepId"] = new SelectList(db.TbDepartamento, "DepId", "DepDescripcion");
            return View();
        }

        // POST: Municipios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("MunId,DepId,MunNombre,MunUsuarioCrea,MunFechaCrea,MunUsuarioModifica,MunFechaModifica")] TbMunicipio tbMunicipio)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbMunicipio_Insert @p0, @p1, @p2, @p3",
                        parameters: new object[] { tbMunicipio.MunId,
                                                   tbMunicipio.DepId,
                                                   tbMunicipio.MunNombre,
                                                     1});
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbMunicipio);
                }

            }
            else
            {
                ViewData["DepId"] = new SelectList(db.TbDepartamento, "DepId", "DepId", tbMunicipio.DepId);

                return View(tbMunicipio);
            }
        }

        // GET: Municipios/Edit/5
        public  IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMunicipio =  db.TbMunicipio.Find(id);
            if (tbMunicipio == null)
            {
                return NotFound();
            }
            ViewData["DepId"] = new SelectList(db.TbDepartamento, "DepId", "DepDescripcion", tbMunicipio.DepId);
            return View(tbMunicipio);
        }

        // POST: Municipios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(string id, [Bind("MunId,DepId,MunNombre,MunUsuarioCrea,MunFechaCrea,MunUsuarioModifica,MunFechaModifica")] TbMunicipio tbMunicipio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbMunicipio_Update @p0, @p1, @p2, @p3",
                        parameters: new object[] { tbMunicipio.MunId,
                                                   tbMunicipio.DepId,
                                                   tbMunicipio.MunNombre,
                                                   1});
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbMunicipio);
                }

            }
            else
            {
                ViewData["DepId"] = new SelectList(db.TbDepartamento, "DepId", "DepId", tbMunicipio.DepId);

                return View(tbMunicipio);
            }
        }

        // GET: Municipios/Delete/5
        public  IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMunicipio =  db.TbMunicipio
                .Include(t => t.Dep)
                .FirstOrDefault(m => m.MunId == id);
            if (tbMunicipio == null)
            {
                return NotFound();
            }

            return View(tbMunicipio);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(string id)
        {
            var tbMunicipio =  db.TbMunicipio.Find(id);
            db.TbMunicipio.Remove(tbMunicipio);
             db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TbMunicipioExists(string id)
        {
            return db.TbMunicipio.Any(e => e.MunId == id);
        }
    }
}
