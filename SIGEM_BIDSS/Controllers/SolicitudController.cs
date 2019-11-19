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

        public ActionResult Create(tbSolicitud SolicitudS, int _tipsol_Id)
        {

            SolicitudS.tipsol_Id = _tipsol_Id;
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

        ///BIND
        [HttpPost]
        [ValidateAntiForgeryToken]
 
        public ActionResult Create([Bind(Include = "emp_Id,tipsol_Id,pto_Id,tpsal_id,tmo_id,are_Id,tipmo_id,tpv_Id,sol_GralDescripcion,sol_GralJefeInmediato,sol_Acper_Anterior,sol_Acper_Nuevo,sol_UsuarioCrea,sol_FechaCrea,sol_UsuarioModifica,sol_FechaModifica,sol_AnsolMonto,pto_Id,tpsal_id,tpv_Id,sol_AnviHospedaje,sol_AnviTrasladoHacia,sol_Anvi_Cliente,sol_Anvi_LugarDestino,sol_Anvi_PropositoVisita,sol_AprCiudad,sol_AprNombreEmpresa,sol_AprRtn,sol_ApreCargoTecn,sol_ApreLink,sol_ApreCorreoAdm,sol_ApreContactoAdm,sol_ApreCorreoTecn,sol_ApreNombreTecn,sol_ApreTelefono,sol_ApreTelefonoTecn,sol_GralComentario,sol_GralJustificacion,sol_ReemNoFactura,sol_ReemProveedor,sol_ReemCargoA,sol_AprDireccion")] tbSolicitud tbSolicitud)
        {
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion");
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
            ViewBag.tmo_id = new SelectList(db.tbTipoMoneda, "tmo_id", "tmo_Abreviatura");
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    var _date = Convert.ToDateTime(tbSolicitud.sol_GralFechaSolicitud).ToShortDateString();
                    list = db.UDP_Gral_CrearAccionPersonal(tbSolicitud.emp_Id, tbSolicitud.tipsol_Id, tbSolicitud.pto_Id, tbSolicitud.tpsal_id,
                        tbSolicitud.tmo_id, tbSolicitud.are_Id, tbSolicitud.tipmo_id, tbSolicitud.tpv_Id, tbSolicitud.sol_GralDescripcion,
                        tbSolicitud.sol_GralJefeInmediato, tbSolicitud.sol_GralCorreoJefeInmediato, tbSolicitud.sol_GralComentario, 
                        tbSolicitud.sol_GralJustificacion, tbSolicitud.sol_GralFechaSolicitud, tbSolicitud.sol_Acper_Anterior,
                        tbSolicitud.sol_Acper_Nuevo, _functions.GetUser(), _functions.DatetimeNow(), _functions.GetUser(),
                        _functions.DatetimeNow());
                    foreach (UDP_Gral_CrearAccionPersonal_Result AccionPersonal in list)
                        MensajeError = AccionPersonal.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {
                    
                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbSolicitud);
                    }
                    else
                    {
                        return RedirectToAction("AccionPersonal");
                    }
                }
                catch (Exception Ex)
                {
       
                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbSolicitud);
                }
            }
            return View(tbSolicitud);
        }

 
        [HttpPost]
        public ActionResult AccionPersonal(tbSolicitud AccionPersonal)
        {

            try
            {
                tbSolicitud vAccionPersonal = db.tbSolicitud.Find(AccionPersonal.sol_Id);
                var MensajeError = "";
                IEnumerable<object> list = null;
                list = db.UDP_Gral_CrearAccionPersonal(AccionPersonal.emp_Id, AccionPersonal.tipsol_Id, AccionPersonal.pto_Id, AccionPersonal.tpsal_id,
                    AccionPersonal.tmo_id, AccionPersonal.are_Id, AccionPersonal.tipmo_id, AccionPersonal.tpv_Id, AccionPersonal.sol_GralDescripcion,
                    AccionPersonal.sol_GralJefeInmediato, AccionPersonal.sol_GralCorreoJefeInmediato, AccionPersonal.sol_GralComentario,
                    AccionPersonal.sol_GralJustificacion, AccionPersonal.sol_GralFechaSolicitud, AccionPersonal.sol_Acper_Anterior, AccionPersonal.sol_Acper_Nuevo,
                    _functions.GetUser(), _functions.DatetimeNow(), _functions.GetUser(), _functions.DatetimeNow());

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
                ModelState.AddModelError("", "No se pudo crear el registro, favor contacte al administrador.");
                return RedirectToAction("AccionPersonal", "Solicitud");
            }
        }
    }
}