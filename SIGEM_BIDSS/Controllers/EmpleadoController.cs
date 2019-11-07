using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class EmpleadoController : Controller
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();

        // GET: Empleado
        public ActionResult Index()
        {
            var tbEmpleado = db.tbEmpleado.Include(t => t.tbMunicipio).Include(t => t.tbPuesto).Include(t => t.tbTipoSangre);
            return View(tbEmpleado.ToList());
        }

        // GET: Empleado/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleado);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo");
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
            ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre");
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,emp_Identificacion,emp_Telefono,emp_CorreoElectronico,tps_Id,pto_Id,emp_FechaIngreso,emp_Direccion,emp_RazonInactivacion,emp_Estado,emp_PathImage,mun_Id,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado)
        {
            if (ModelState.IsValid)
            {
                db.tbEmpleado.Add(tbEmpleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbEmpleado.mun_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
            ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
            return View(tbEmpleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbEmpleado.mun_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
            ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
            return View(tbEmpleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_Id,emp_Nombres,emp_Apellidos,emp_Sexo,emp_FechaNacimiento,emp_Identificacion,emp_Telefono,emp_CorreoElectronico,tps_Id,pto_Id,emp_FechaIngreso,emp_Direccion,emp_RazonInactivacion,emp_Estado,emp_PathImage,mun_Id,emp_UsuarioCrea,emp_FechaCrea,emp_UsuarioModifica,emp_FechaModifica")] tbEmpleado tbEmpleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbEmpleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mun_Id = new SelectList(db.tbMunicipio, "mun_codigo", "dep_codigo", tbEmpleado.mun_Id);
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion", tbEmpleado.pto_Id);
            ViewBag.tps_Id = new SelectList(db.tbTipoSangre, "tps_Id", "tps_nombre", tbEmpleado.tps_Id);
            return View(tbEmpleado);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            if (tbEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tbEmpleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbEmpleado tbEmpleado = db.tbEmpleado.Find(id);
            db.tbEmpleado.Remove(tbEmpleado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
