using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class EmpleadoController : Controller
    {
        SIGEM_BIDSSModel db = new SIGEM_BIDSSModel();

        

        // GET: Empleado
        public IActionResult Index()
        {
            var sIGEM_BIDSSModel = db.TbEmpleado.Include(t => t.Mun).Include(t => t.Pto).Include(t => t.Tps);
            return View(sIGEM_BIDSSModel);
        }

        // GET: Empleado/Details/5
        public IActionResult Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEmpleado =  db.TbEmpleado
                .Include(t => t.Mun)
                .Include(t => t.Pto)
                .Include(t => t.Tps)
                .FirstOrDefault(m => m.EmpId == id);
            if (tbEmpleado == null)
            {
                return NotFound();
            }

            return View(tbEmpleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            ViewData["DepId"] = new SelectList(db.TbDepartamento, "DepId", "DepDescripcion");
            ViewData["PtoId"] = new SelectList(db.TbPuesto, "PtoId", "PtoDescripcion");
            ViewData["TpsId"] = new SelectList(db.TbTipoSangre, "TpsId", "TpsNombre");
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmpId,EmpNombres,EmpApellidos,EmpSexo,EmpFechaNacimiento,EmpIdentificacion,EmpTelefono,EmpCorreoElectronico,TpsId,PtoId,EmpFechaIngreso,EmpDireccion,EmpRazonInactivacion,EmpEstado,EmpPathImage,MunId,EmpUsuarioCrea,EmpFechaCrea,EmpUsuarioModifica,EmpFechaModifica,DepId")] TbEmpleado tbEmpleado)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbEmpleado_Insert @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15" +
                        "", parameters: new object[] { tbEmpleado.EmpNombres,
                                                       tbEmpleado.EmpApellidos,
                                                       tbEmpleado.EmpSexo,
                                                       tbEmpleado.EmpFechaNacimiento,
                                                       tbEmpleado.EmpIdentificacion,
                                                       tbEmpleado.EmpTelefono,
                                                       tbEmpleado.EmpCorreoElectronico,
                                                       tbEmpleado.TpsId,
                                                       tbEmpleado.PtoId,
                                                       tbEmpleado.EmpFechaIngreso,
                                                       tbEmpleado.EmpDireccion,
                                                       tbEmpleado.EmpRazonInactivacion,
                                                       tbEmpleado.EmpEstado,
                                                       tbEmpleado.EmpPathImage,
                                                       tbEmpleado.MunId,
                                                       tbEmpleado.EmpUsuarioCrea
                        });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    if (tbEmpleado.MunId == null)
                    {
                        ViewData["MunId"] = null;
                    }
                    else
                    {
                        ViewData["MunId"] = new SelectList(db.TbMunicipio, "MunId", "MunNombre", tbEmpleado.MunId);
                    }
                    ViewData["DepId"] = new SelectList(db.TbDepartamento, "DepId", "DepDescripcion", tbEmpleado.DepId);
                    ViewData["PtoId"] = new SelectList(db.TbPuesto, "PtoId", "PtoDescripcion", tbEmpleado.PtoId);
                    ViewData["TpsId"] = new SelectList(db.TbTipoSangre, "TpsId", "TpsNombre", tbEmpleado.TpsId);

                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbEmpleado);
                }

            }
            else
            {
                if (tbEmpleado.MunId == null)
                {
                    ViewData["MunId"] = null;
                }
                else
                {
                    ViewData["MunId"] = new SelectList(db.TbMunicipio, "MunId", "MunNombre", tbEmpleado.MunId);
                }
                ViewData["DepId"] = new SelectList(db.TbDepartamento, "DepId", "DepDescripcion", tbEmpleado.DepId);
                ViewData["PtoId"] = new SelectList(db.TbPuesto, "PtoId", "PtoDescripcion", tbEmpleado.PtoId);
                ViewData["TpsId"] = new SelectList(db.TbTipoSangre, "TpsId", "TpsNombre", tbEmpleado.TpsId);

                return View(tbEmpleado);
            }
            
        }

        // GET: Empleado/Edit/5
        public IActionResult Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEmpleado =  db.TbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return NotFound();
            }
            ViewData["MunId"] = new SelectList(db.TbMunicipio, "MunId", "MunId", tbEmpleado.MunId);
            ViewData["PtoId"] = new SelectList(db.TbPuesto, "PtoId", "PtoDescripcion", tbEmpleado.PtoId);
            ViewData["TpsId"] = new SelectList(db.TbTipoSangre, "TpsId", "TpsNombre", tbEmpleado.TpsId);
            return View(tbEmpleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(short id, [Bind("EmpId,EmpNombres,EmpApellidos,EmpSexo,EmpFechaNacimiento,EmpIdentificacion,EmpTelefono,EmpCorreoElectronico,TpsId,PtoId,EmpFechaIngreso,EmpDireccion,EmpRazonInactivacion,EmpEstado,EmpPathImage,MunId,EmpUsuarioCrea,EmpFechaCrea,EmpUsuarioModifica,EmpFechaModifica")] TbEmpleado tbEmpleado)
        {
            if (id != tbEmpleado.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("Gral.UDP_Gral_tbEmpleado_Update @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16" +
                        "", parameters: new object[] { tbEmpleado.EmpId,
                                                       tbEmpleado.EmpNombres,
                                                       tbEmpleado.EmpApellidos,
                                                       tbEmpleado.EmpSexo,
                                                       tbEmpleado.EmpFechaNacimiento,
                                                       tbEmpleado.EmpIdentificacion,
                                                       tbEmpleado.EmpTelefono,
                                                       tbEmpleado.EmpCorreoElectronico,
                                                       tbEmpleado.TpsId,
                                                       tbEmpleado.PtoId,
                                                       tbEmpleado.EmpFechaIngreso,
                                                       tbEmpleado.EmpDireccion,
                                                       tbEmpleado.EmpRazonInactivacion,
                                                       tbEmpleado.EmpEstado,
                                                       tbEmpleado.EmpPathImage,
                                                       tbEmpleado.MunId,
                                                       tbEmpleado.EmpUsuarioModifica
                        });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    ViewData["MunId"] = new SelectList(db.TbMunicipio, "MunId", "MunId", tbEmpleado.MunId);
                    ViewData["PtoId"] = new SelectList(db.TbPuesto, "PtoId", "PtoDescripcion", tbEmpleado.PtoId);
                    ViewData["TpsId"] = new SelectList(db.TbTipoSangre, "TpsId", "TpsNombre", tbEmpleado.TpsId);

                    //Function.InsertBitacoraErrores("Empleado/Create", Ex.Message.ToString(), "Create");
                    var Message = Ex.Message;
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbEmpleado);
                }

            }
            else
            {
                ViewData["MunId"] = new SelectList(db.TbMunicipio, "MunId", "MunId", tbEmpleado.MunId);
                ViewData["PtoId"] = new SelectList(db.TbPuesto, "PtoId", "PtoDescripcion", tbEmpleado.PtoId);
                ViewData["TpsId"] = new SelectList(db.TbTipoSangre, "TpsId", "TpsNombre", tbEmpleado.TpsId);

                return View(tbEmpleado);
            }
        }

        // GET: Empleado/Delete/5
        public IActionResult Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEmpleado =  db.TbEmpleado
                .Include(t => t.Mun)
                .Include(t => t.Pto)
                .Include(t => t.Tps)
                .FirstOrDefault(m => m.EmpId == id);
            if (tbEmpleado == null)
            {
                return NotFound();
            }

            return View(tbEmpleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            var tbEmpleado =  db.TbEmpleado.Find(id);
            db.TbEmpleado.Remove(tbEmpleado);
             db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TbEmpleadoExists(short id)
        {
            return db.TbEmpleado.Any(e => e.EmpId == id);
        }



        [HttpPost]
        public JsonResult GetValue(string DepId)
        {
            var _result = db.TbMunicipio
            .Where(x => x.DepId == DepId)
            .Select(y => new TbMunicipio() {  MunId= y.MunId ,MunNombre = y.MunNombre })
            .ToList();

            var _resultTwo = db.TbMunicipio.Where(x => x.DepId == DepId).ToArray();

            return new JsonResult(_result);
        }

    }
}
