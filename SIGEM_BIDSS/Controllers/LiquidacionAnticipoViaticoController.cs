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
    public class LiquidacionAnticipoViaticoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: LiquidacionAnticipoViatico
        public ActionResult Index()
        {
            var tbAnticipoViatico = db.tbAnticipoViatico.Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbMunicipio).Include(t => t.tbTipoTransporte);
            return View(tbAnticipoViatico.ToList());
        }

        // GET: LiquidacionAnticipoViatico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            if (tbLiquidacionAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViatico);
        }

        // GET: LiquidacionAnticipoViatico/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnticipoViatico tbAnticipoViatico = db.tbAnticipoViatico.Find(id);
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = new tbLiquidacionAnticipoViatico();
            tbLiquidacionAnticipoViatico.Anvi_Id = tbAnticipoViatico.Anvi_Id;


            if (tbAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViatico);
        }

    

    // POST: LiquidacionAnticipoViatico/Create
    // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
    // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lianvi_Id,Lianvi_Correlativo,Anvi_Id,Lianvi_FechaLiquida,Lianvi_FechaInicioViaje,Lianvi_FechaFinViaje,Lianvi_Comentario,est_Id,Lianvi_RazonRechazo")] tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico)
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
                    _List = db.UDP_Adm_tbLiquidacionAnticipoViatico_Insert(tbLiquidacionAnticipoViatico.Anvi_Id, 
                                                                          tbLiquidacionAnticipoViatico.Lianvi_FechaLiquida,
                                                                          tbLiquidacionAnticipoViatico.Lianvi_FechaInicioViaje,
                                                                          tbLiquidacionAnticipoViatico.Lianvi_FechaFinViaje,
                                                                          tbLiquidacionAnticipoViatico.Lianvi_Comentario,
                                                                           GeneralFunctions.Enviada, 
                                                                          tbLiquidacionAnticipoViatico.Lianvi_RazonRechazo,
                                                                          EmployeeID, Function.DatetimeNow());
                    foreach (UDP_Adm_tbLiquidacionAnticipoViatico_Insert_Result Area in _List)
                        ErrorMessage = Area.MensajeError;
                    if (ErrorMessage.StartsWith("-1"))
                    {
                        Function.BitacoraErrores("LiquidacionAnticipoViatico", "CreatePost", UserName, ErrorMessage);
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbLiquidacionAnticipoViatico);
                    }
            
                    else
                    {
                        var GetEmployee = db.tbEmpleado.Where(x => x.emp_Id == EmployeeID).Select(x => new { emp_Nombres = x.emp_Nombres + " " + x.emp_Apellidos, x.emp_CorreoElectronico }).FirstOrDefault();
                        var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                        Result = Function.LeerDatos(out ErrorEmail, ErrorMessage, GetEmployee.emp_Nombres, "", GetEmployee.emp_CorreoElectronico, GeneralFunctions.msj_Enviada, "");
                        ResultAdm = Function.LeerDatos(out ErrorEmail, ErrorMessage, _Parameters.par_NombreEmpresa, GetEmployee.emp_Nombres, _Parameters.par_CorreoEmpresa, GeneralFunctions.msj_ToAdmin, "");

                        if (!Result) Function.BitacoraErrores("LiquidacionAnticipoViatico", "CreatePost", UserName, ErrorEmail);
                        if (!ResultAdm) Function.BitacoraErrores("LiquidacionAnticipoViatico", "CreatePost", UserName, ErrorEmail);
                        TempData["swalfunction"] = "true";

                        return RedirectToAction("Index");
                    }

                }


                catch (Exception Ex)
                {
                    Function.BitacoraErrores("LiquidacionAnticipoViatico", "CreatePost", UserName, Ex.Message.ToString());
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbLiquidacionAnticipoViatico);
                }
            }
            return View(tbLiquidacionAnticipoViatico);
        }
// GET: LiquidacionAnticipoViatico/Edit/5
public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            if (tbLiquidacionAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            ViewBag.Anvi_Id = new SelectList(db.tbAnticipoViatico, "Anvi_Id", "Anvi_Correlativo", tbLiquidacionAnticipoViatico.Anvi_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbLiquidacionAnticipoViatico.est_Id);
            return View(tbLiquidacionAnticipoViatico);
        }

        // POST: LiquidacionAnticipoViatico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lianvi_Id,Lianvi_Correlativo,Anvi_Id,Lianvi_FechaLiquida,Lianvi_FechaInicioViaje,Lianvi_FechaFinViaje,Lianvi_Comentario,est_Id,Lianvi_RazonRechazo,Lianvi_UsuarioCrea,Lianvi_FechaCrea,Lianvi_UsuarioModifica,Lianvi_FechaModifica")] tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbLiquidacionAnticipoViatico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Anvi_Id = new SelectList(db.tbAnticipoViatico, "Anvi_Id", "Anvi_Correlativo", tbLiquidacionAnticipoViatico.Anvi_Id);
            ViewBag.est_Id = new SelectList(db.tbEstado, "est_Id", "est_Descripcion", tbLiquidacionAnticipoViatico.est_Id);
            return View(tbLiquidacionAnticipoViatico);
        }

        // GET: LiquidacionAnticipoViatico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            if (tbLiquidacionAnticipoViatico == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViatico);
        }

        // POST: LiquidacionAnticipoViatico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = db.tbLiquidacionAnticipoViatico.Find(id);
            db.tbLiquidacionAnticipoViatico.Remove(tbLiquidacionAnticipoViatico);
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
