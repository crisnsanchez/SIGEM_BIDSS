using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers

{
    [Authorize]
    [SessionManager]
    public class LiquidacionAnticipoViaticoController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: LiquidacionAnticipoViatico
        public ActionResult Index()
        {
            
            var tbAnticipoViatico = db.tbAnticipoViatico.Where(t=>t.est_Id==GeneralFunctions.AprobadaPorJefe).Include(t => t.tbEmpleado).Include(t => t.tbEmpleado1).Include(t => t.tbMunicipio).Include(t => t.tbTipoTransporte);
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



        [HttpPost]
        public JsonResult CalcularFecha(cCalFechas cCalFechas)
        {

            string MASspan = "",  MASspan1 = "", MASspanFecha = "";
            if (cCalFechas.FechaInicio >= cCalFechas.FechaFin)
            {
                MASspanFecha = "1";
                MASspan1 = "La Fecha de regreso no puede ser menor que la inicio";
            }
           if (cCalFechas.FechaFin >= Function.DatetimeNow())
            {
                MASspan1 = "";
                   MASspanFecha = "2";
                MASspan = "La Fecha de regreso no puede ser igual mayor a la fecha actual";
            }
            if (cCalFechas.FechaInicio >= Function.DatetimeNow())
            {
                MASspan1 = "";
                MASspanFecha = "2";
                MASspan = "La Fecha de inicio no puede ser mayor que la  fecha de regreso";
            }
            object vCalcular = new { MASspan, MASspan1, MASspanFecha };
            return Json(vCalcular, JsonRequestBehavior.AllowGet);
        }

        // GET: LiquidacionAnticipoViatico/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ViewBag.uni_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            tbAnticipoViatico tbAnticipoViatico = db.tbAnticipoViatico.Find(id);
            tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico = new tbLiquidacionAnticipoViatico();
            tbLiquidacionAnticipoViatico.Anvi_Id = tbAnticipoViatico.Anvi_Id;
            tbLiquidacionAnticipoViatico.Lianvi_FechaLiquida = Function.DatetimeNow();
            tbLiquidacionAnticipoViatico.Lianvi_FechaInicioViaje = Function.DatetimeNow();
            tbLiquidacionAnticipoViatico.Lianvi_FechaFinViaje = Function.DatetimeNow();

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
        public ActionResult Create([Bind(Include = "Lianvi_Id,Lianvi_Correlativo,Anvi_Id,Lianvi_FechaLiquida,Lianvi_FechaInicioViaje,Lianvi_FechaFinViaje,Lianvi_Comentario,est_Id,Lianvi_RazonRechazo")] tbLiquidacionAnticipoViatico tbLiquidacionAnticipoViatico,int? Id)
        {
           
            {
                string UserName = "",
                ErrorEmail = "";
                try
                {
                    cGetUserInfo GetEmployee = null;
                    cGetUserInfo EmpJefe = null;
                    bool Result = false, ResultAdm = false;
                
                    if (ModelState.IsValid)
                    {
                        int EmployeeID = Function.GetUser(out UserName);
                      

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
                            

                    

                            TempData["swalfunction"] = "true";

                            
                            Session["NombreLiquidacione"] = ErrorMessage;


                            return RedirectToAction("Create", "LiquidacionAnticipoViaticoDetalle");


                        }

                    }
                }

                catch (Exception Ex)
                {
                    Function.BitacoraErrores("LiquidacionAnticipoViatico", "CreatePost", UserName, Ex.Message.ToString());

                    return RedirectToAction("Create", "LiquidacionAnticipoViaticoDetalle");
                }
            }
            return RedirectToAction("Create", "LiquidacionAnticipoViaticoDetalle");
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
