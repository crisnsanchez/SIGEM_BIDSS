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
    [Authorize]
    [SessionManager]
    public class SolicitudReembolsoGastosController : BaseController
 
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();
        // GET: SolicitudReembolsoGastos
        public ActionResult Index()
        {
            var tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Include(t => t.tbEmpleado).Include(t => t.tbEstado);
            return View(tbSolicitudReembolsoGastos.ToList());
        }

        // GET: SolicitudReembolsoGastos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Find(id);
            if (tbSolicitudReembolsoGastos == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastos);
        }

        // GET: SolicitudReembolsoGastos/Create
        public ActionResult Create(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastos tbSolicitudReembolso = db.tbSolicitudReembolsoGastos.Find(id);
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = new tbSolicitudReembolsoGastos();
            tbSolicitudReembolsoGastos.Reemga_Id = tbSolicitudReembolso.Reemga_Id;


            if (tbSolicitudReembolso == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastos);
            //ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            //ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion");
            //return View();
        }

        // POST: SolicitudReembolsoGastos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reemga_Id,Reemga_Correlativo,emp_Id,emp_EsJefe,Reemga_GralFechaSolicitud," +
            "Reemga_FechaViaje,Reemga_Cliente,mun_codigo,Reemga_PropositoVisita,Reemga_DiasVisita,Reemga_Comentario,est_Id," +
            "Reemga_RazonRechazo,Reemga_UsuarioCrea,Reemga_FechaCrea")] tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos)
        {
            if (ModelState.IsValid)
            {
                string UserName = "",
               ErrorEmail = "";
                try
                {
                    int EmployeeID = Function.GetUser(out UserName);
                    bool Result = false, ResultAdm = false;

                    IEnumerable<object> _List = null;
                    string ErrorMessage = "";
                    _List = db.UDP_Adm_tbSolicitudReembolsoGastos_Insert(tbSolicitudReembolsoGastos.Reemga_Correlativo,tbSolicitudReembolsoGastos.emp_Id,
                        tbSolicitudReembolsoGastos.Reemga_GralFechaSolicitud,tbSolicitudReembolsoGastos.Reemga_FechaViaje,tbSolicitudReembolsoGastos.Reemga_Cliente,
                        tbSolicitudReembolsoGastos.mun_codigo,tbSolicitudReembolsoGastos.Reemga_PropositoVisita,tbSolicitudReembolsoGastos.Reemga_DiasVisita,
                        tbSolicitudReembolsoGastos.Reemga_Comentario,tbSolicitudReembolsoGastos.est_Id,tbSolicitudReembolsoGastos.Reemga_RazonRechazo, EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Adm_tbSolicitudReembolsoGastos_Insert_Result Reembolso in _List)
                        ErrorMessage = Reembolso.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("SolicitudReembolsoGastos", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbSolicitudReembolsoGastos);
                    }

                    else
                    {
                        var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                        var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, "", GetEmployee.emp_CorreoElectronico, GeneralFunctions.msj_Enviada, "");
                        ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GetEmployee.emp_Nombres, _Parameters.par_CorreoEmpresa, GeneralFunctions.msj_ToAdmin, "");

                        if (!Result) Function.BitacoraErrores("SolicitudReembolsoGastos", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Function.BitacoraErrores("SolicitudReembolsoGastos", "CreatePost", UserName, ErrorEmail);
                        TempData["swalfunction"] = "true";

                        return RedirectToAction("Index");
                    }

                }


                catch (Exception Ex)
                {
                    Function.BitacoraErrores("SolicitudReembolsoGastos", "CreatePost", UserName, Ex.Message.ToString());
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbSolicitudReembolsoGastos);
                }
                //db.tbSolicitudReembolsoGastos.Add(tbSolicitudReembolsoGastos);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSolicitudReembolsoGastos.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbSolicitudReembolsoGastos.est_Id);
            return View(tbSolicitudReembolsoGastos);
        }

        // GET: SolicitudReembolsoGastos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Find(id);
            if (tbSolicitudReembolsoGastos == null)
            {
                return HttpNotFound();
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSolicitudReembolsoGastos.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbSolicitudReembolsoGastos.est_Id);
            return View(tbSolicitudReembolsoGastos);
        }

        // POST: SolicitudReembolsoGastos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Reemga_Id,Reemga_Correlativo,emp_Id,emp_EsJefe,Reemga_GralFechaSolicitud,Reemga_FechaViaje,Reemga_Cliente,mun_codigo,Reemga_PropositoVisita,Reemga_DiasVisita,Reemga_Comentario,est_Id,Reemga_RazonRechazo,Reemga_UsuarioModifica,Reemga_FechaModifica")] tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitudReembolsoGastos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres", tbSolicitudReembolsoGastos.emp_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbSolicitudReembolsoGastos.est_Id);
            return View(tbSolicitudReembolsoGastos);
        }

        // GET: SolicitudReembolsoGastos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Find(id);
            if (tbSolicitudReembolsoGastos == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitudReembolsoGastos);
        }

        // POST: SolicitudReembolsoGastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitudReembolsoGastos tbSolicitudReembolsoGastos = db.tbSolicitudReembolsoGastos.Find(id);
            db.tbSolicitudReembolsoGastos.Remove(tbSolicitudReembolsoGastos);
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
