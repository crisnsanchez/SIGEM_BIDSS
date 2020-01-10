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
    public class RequisionCompraDetalleController : BaseController
    {
        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        GeneralFunctions Function = new GeneralFunctions();

        // GET: RequisionCompraDetalle/Create
        public ActionResult Create()
        {
            try
            {
                int Id = Convert.ToInt32(Session["Reqco_Id"]);
                if (Id==0)
                {
                    return RedirectToAction("Create", "RequisionCompra");
                }
                tbRequisionCompraDetalle tbRequisionCompraDetalle = new tbRequisionCompraDetalle();
                tbRequisionCompraDetalle.Reqco_Id = Id;
                ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Descripcion");
                ViewBag.Producto = db.tbProducto.Where(x => x.prod_EsActivo == true).ToList();
                return View();
            }
            catch (Exception)
            {
                throw;
                return View();
            }
        }

        // POST: RequisionCompraDetalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reqco_Id,prod_Id,Reqde_Cantidad,Reqde_Justificacion")] tbRequisionCompraDetalle tbRequisionCompraDetalle)
        {
            IEnumerable<object> List = null;
            string UserName = "";
            string MensajeError = "";
            var listaDetalle = (List<tbRequisionCompraDetalle>)Session["RequisionCompraDetalle"];
            try
            {
                ViewBag.prod_Id = new SelectList(db.tbProducto, "prod_Id", "prod_Descripcion");
                ViewBag.Producto = db.tbProducto.Where(x => x.prod_EsActivo == true).ToList();
                int EmployeeID = Function.GetUser(out UserName);
                if (listaDetalle != null)
                {
                    if (listaDetalle.Count > 0)
                    {
                        foreach (tbRequisionCompraDetalle RequisionCompraDetalle in listaDetalle)
                        {
                            List = db.UDP_Adm_tbRequisionCompraDetalle_Insert(RequisionCompraDetalle.Reqco_Id,
                                RequisionCompraDetalle.prod_Id, RequisionCompraDetalle.Cantidad, RequisionCompraDetalle.Reqde_Justificacion, EmployeeID, Function.DatetimeNow());
                            foreach (UDP_Adm_tbRequisionCompraDetalle_Insert_Result RequisionCompra in List)
                                MensajeError = RequisionCompra.MensajeError;
                            if (MensajeError.StartsWith("-1"))
                            {

                                ModelState.AddModelError("", "No se pudo insertar el registro detalle, favor contacte al administrador.");
                                return View(tbRequisionCompraDetalle);
                            }
                        }
                    }
                }


            }

            catch (Exception Ex)
            {
                Function.BitacoraErrores("RequisionCompraDetalle", "CreatePost", UserName, Ex.Message.ToString());
            }
            return View(tbRequisionCompraDetalle);
        }


        //GetDetalle
        [HttpPost]
        public JsonResult SaveCompraDetalle(tbRequisionCompraDetalle tbRequisionCompraDetalle)
        {
            int datos = 0;
            decimal CantidadVieja = 0;
            decimal CantidadNueva = 0;
            //data_producto = SalidaDetalle.prod_Codigo;
            decimal Reqde_Cantidad = tbRequisionCompraDetalle.Cantidad;
            List<tbRequisionCompraDetalle> sessionRequisionCompraDetalle = new List<tbRequisionCompraDetalle>();
            var list = (List<tbRequisionCompraDetalle>)Session["RequisionCompraDetalle"];

            if (list == null)
            {
                sessionRequisionCompraDetalle.Add(tbRequisionCompraDetalle);
                Session["RequisionCompraDetalle"] = sessionRequisionCompraDetalle;
            }
            else
            {
                foreach (var CompraDetalle in list)
                    if (CompraDetalle.prod_Id == tbRequisionCompraDetalle.prod_Id)
                    {
                        datos = tbRequisionCompraDetalle.prod_Id;
                        foreach (var viejo in list)
                            if (viejo.prod_Id == datos)
                                CantidadVieja = viejo.Cantidad;
                        CantidadNueva = CantidadVieja + tbRequisionCompraDetalle.Cantidad;
                        CompraDetalle.Cantidad = CantidadNueva;
                        return Json(datos, JsonRequestBehavior.AllowGet);
                    }
                list.Add(tbRequisionCompraDetalle);
                Session["ReembolsoDetalle"] = list;
                return Json(datos, JsonRequestBehavior.AllowGet);
            }
            return Json(datos, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Remove(tbRequisionCompraDetalle tbRequisionCompraDetalle)
        {
            var list = (List<tbRequisionCompraDetalle>)Session["RequisionCompraDetalle"];

            if (list != null)
            {
                var itemToRemove = list.Single(r => r.prod_Id == tbRequisionCompraDetalle.prod_Id);
                list.Remove(itemToRemove);
                Session["tbRequisionCompraDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
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
