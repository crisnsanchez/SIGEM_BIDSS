using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using SIGEM_BIDSS.Models;

namespace SIGEM_BIDSS.Controllers
{
    public class LiquidacionAnticipoViaticoDetalleController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();


        // GET: LiquidacionAnticipoViaticoDetalle
        public ActionResult Index()
        {
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            var tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Include(t => t.tbLiquidacionAnticipoViatico).Include(t => t.tbTipoViatico);
            return View(tbLiquidacionAnticipoViaticoDetalle.ToList());
        }

        //GET: LiquidacionAnticipoViaticoDetalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        //GET: LiquidacionAnticipoViaticoDetalle/Create
        public ActionResult Create( )
        {
            

         int Id =  Convert.ToInt32(Session["NombreLiquidacione"]);
   
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = new tbLiquidacionAnticipoViaticoDetalle();
            tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id = Id;
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            tbLiquidacionAnticipoViaticoDetalle.Lianvide_FechaGasto = Function.DatetimeNow();
         

            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViaticoDetalle);
         

        }


        // POST: LiquidacionAnticipoViaticoDetalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public JsonResult SaveLiquidacionAnticipoDetalle(tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle, HttpPostedFileBase Archivo)
        {
            int datos = 0;

            decimal Lianvide_MontoGasto = tbLiquidacionAnticipoViaticoDetalle.Lianvide_MontoGasto;
            List<tbLiquidacionAnticipoViaticoDetalle> sessionSolicitudReembolsoDetalle = new List<tbLiquidacionAnticipoViaticoDetalle>();
            var list = (List<tbLiquidacionAnticipoViaticoDetalle>)Session["NombreLiquidaciondetalle"];

            if (list == null)
            {
                sessionSolicitudReembolsoDetalle.Add(tbLiquidacionAnticipoViaticoDetalle);
                Session["NombreLiquidaciondetalle"] = sessionSolicitudReembolsoDetalle;
            }
            else
            {

                list.Add(tbLiquidacionAnticipoViaticoDetalle);
                Session["NombreLiquidaciondetalle"] = list;
                return Json(tbLiquidacionAnticipoViaticoDetalle, JsonRequestBehavior.AllowGet);
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lianvide_Id,Lianvi_Id,Lianvide_FechaGasto,tpv_Id,Lianvide_MontoGasto,Lianvide_Concepto,Lianvide_UsuarioCrea,Lianvide_FechaCrea,Lianvide_UsuarioModifica,Lianvide_FechaModifica")] tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle, HttpPostedFileBase ArchivoPath)
        {


            string UserName = "";
            string MsjError = "";
            string ErrorEmail = "";
            var listaLiquidacion = (List<tbLiquidacionAnticipoViaticoDetalle>)Session["NombreLiquidaciondetalle"];
            try
            {
                cGetUserInfo GetEmployee = null;
                cGetUserInfo EmpJefe = null;
                bool Result = false, ResultAdm = false;
                ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
                IEnumerable<object> lista = null;
                int EmployeeID = Function.GetUser(out UserName);
                if (listaLiquidacion != null)
                {
                    if (listaLiquidacion.Count > 0)
                    {
                        foreach (tbLiquidacionAnticipoViaticoDetalle Sol in listaLiquidacion)
                        {
                            lista = db.UDP_Adm_tbLiquidacionAnticipoViaticoDetalle_Insert(tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id, Sol.Lianvide_FechaGasto, Sol.tpv_Id, Sol.Lianvide_MontoGasto, Sol.Lianvide_Concepto, Sol.Lianvide_Archivo, EmployeeID, Function.DatetimeNow());
                            foreach (UDP_Adm_tbLiquidacionAnticipoViaticoDetalle_Insert_Result SolicitudDetalle in lista)
                                MsjError = SolicitudDetalle.MensajeError;
                            if (MsjError.StartsWith("-1"))
                            {

                                ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                return View(tbLiquidacionAnticipoViaticoDetalle);
                            }
                        }
                    }
                }

                GetEmployee = Function.GetUserInfo(EmployeeID);
                EmpJefe = Function.GetUserInfo(EmployeeID);

                Result = Function.LeerDatos(out ErrorEmail, MsjError, GetEmployee.emp_Nombres, GeneralFunctions.stringEmpty, GeneralFunctions.msj_Enviada, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, GetEmployee.emp_CorreoElectronico);
                ResultAdm = Function.LeerDatos(out ErrorEmail, MsjError, EmpJefe.emp_Nombres, GetEmployee.emp_Nombres, GeneralFunctions.msj_ToAdmin, GeneralFunctions.stringEmpty, GeneralFunctions.stringEmpty, EmpJefe.emp_CorreoElectronico);


                if (!Result) Function.BitacoraErrores("AccionPersonal", "CreatePost", UserName, ErrorEmail);
                if (!ResultAdm) Function.BitacoraErrores("AccionPersonal", "CreatePost", UserName, ErrorEmail);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["swalfunction"] = GeneralFunctions._isCreated;
                ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Id");
                ViewBag.Reemga_Id = new SelectList(db.tbSolicitudReembolsoGastos, "Reemga_Id", "Reemga_Correlativo");
                ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
                ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                return View(tbLiquidacionAnticipoViaticoDetalle);
            }

        }


        // GET: LiquidacionAnticipoViaticoDetalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo", tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbLiquidacionAnticipoViaticoDetalle.tpv_Id);
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // POST: LiquidacionAnticipoViaticoDetalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lianvide_Id,Lianvi_Id,Lianvide_FechaGasto,tpv_Id,Lianvide_MontoGasto,Lianvide_Concepto,Lianvide_Archivo,Lianvide_UsuarioCrea,Lianvide_FechaCrea,Lianvide_UsuarioModifica,Lianvide_FechaModifica")] tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbLiquidacionAnticipoViaticoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Lianvi_Id = new SelectList(db.tbLiquidacionAnticipoViatico, "Lianvi_Id", "Lianvi_Correlativo", tbLiquidacionAnticipoViaticoDetalle.Lianvi_Id);
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion", tbLiquidacionAnticipoViaticoDetalle.tpv_Id);
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // GET: LiquidacionAnticipoViaticoDetalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            if (tbLiquidacionAnticipoViaticoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(tbLiquidacionAnticipoViaticoDetalle);
        }

        // POST: LiquidacionAnticipoViaticoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbLiquidacionAnticipoViaticoDetalle tbLiquidacionAnticipoViaticoDetalle = db.tbLiquidacionAnticipoViaticoDetalle.Find(id);
            db.tbLiquidacionAnticipoViaticoDetalle.Remove(tbLiquidacionAnticipoViaticoDetalle);
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

        [HttpPost]
        public JsonResult RemoveDetalle(tbLiquidacionAnticipoViaticoDetalle LiquidacionDetalle)
        {
            var list = (List<tbLiquidacionAnticipoViaticoDetalle>)Session["LiquidacionAnticipoViaticoDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.Lianvide_Id == LiquidacionDetalle.Lianvide_Id);
                list.Remove(itemToRemove);
     
                Session["LiquidacionAnticipoViaticoDetalle"] = list;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }


}
