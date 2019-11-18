using SIGEM_BIDSS.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SIGEM_BIDSS.Controllers
{
    [Authorize]
    public class SolicitudController : Controller
    {

        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        private GeneralFunctions _functions = new GeneralFunctions();


        // GET: Solicitud
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _PermisoLaboral(int meter_id)
        {
            return PartialView(meter_id);
        }

        public ActionResult _CheckListCapacitaciones(int meter_id)
        {
               return PartialView(meter_id);
        }

        public ActionResult _AnticipoViatico(int meter_id)
        {
            return PartialView(meter_id);
        }

        public ActionResult _RembolsoGastos(int meter_id)
        {
            return PartialView(meter_id);
        }
        public ActionResult _LiquidacionAnticipoViaticos(int meter_id)
        {
            return PartialView(meter_id);
        }
        public ActionResult _AnticipoSalario(int meter_id)
        {
            return PartialView(meter_id);
        }



        public ActionResult Create(tbSolicitud SolicitudS, string _tipsol_Id)
        {

            SolicitudS.tipsol_Id = _functions.TipodeSolicitudSearch(_tipsol_Id);
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion");
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
            ViewBag.tmo_id = new SelectList(db.tbTipoMoneda, "tmo_id", "tmo_Abreviatura");
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");

            return View(SolicitudS);
        }

        [HttpGet]
        public ActionResult _AccionPersonal(int meter_id)
        {
            return PartialView(meter_id);
        }

        [HttpPost]
        public ActionResult AccionPersonal(tbSolicitud AccionPersonal)
        {
            //public ActionResult UpdateFacturaDetalle(tbFacturaDetalle EditFacturaDetalle)
            try
            {
                tbSolicitud vAccionPersonal = db.tbSolicitud.Find(AccionPersonal.sol_Id);
                var MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Gral_CrearAccionPersonal(AccionPersonal.emp_Id, AccionPersonal.tipsol_Id, AccionPersonal.pto_Id, AccionPersonal.tpsal_id,
                    AccionPersonal.tmo_id, AccionPersonal.are_Id, AccionPersonal.tipmo_id, AccionPersonal.tpv_Id, AccionPersonal.sol_GralDescripcion,
                    AccionPersonal.sol_GralJefeInmediato, AccionPersonal.sol_GralCorreoJefeInmediato, AccionPersonal.sol_GralComentario,
                    AccionPersonal.sol_GralJustificacion, AccionPersonal.sol_GralFechaSolicitud, AccionPersonal.sol_Acper_Anterior, AccionPersonal.sol_Acper_Nuevo,
                    _functions.sol_UsuarioCrea(), _functions.DatetimeNow(), _functions.sol_UsuarioCrea(), _functions.DatetimeNow());

                foreach (UDP_Gral_CrearAccionPersonal_Result AccionPersonals in list)
                    MensajeError = AccionPersonals.MensajeError;
                if (MensajeError == "-1")
                {
                    //Function.InsertBitacoraErrores("Factura/Edit", MensajeError, "Edit");
                    ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                    return RedirectToAction("AccionPersonal", "Solicitud");
                }
                else
                {
                    return RedirectToAction("AccionPersonal", "Solicitud");
                }
            }
            catch (Exception Ex)
            {



                Ex.Message.ToString();
                ModelState.AddModelError("", "No se pudo actualizar el registro, favor contacte al administrador.");
                return RedirectToAction("Edit", "Factura");
            }
        
        }
    }
}