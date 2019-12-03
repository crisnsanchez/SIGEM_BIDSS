﻿using SIGEM_BIDSS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;

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

        public ActionResult Create(tbSolicitud tbSolicitud, int _tipsol_Id)
        {
           
            try
            {

                string lvMensajeError = "";

                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

                string fullName = userClaims?.FindFirst("name")?.Value;
                string[] names = fullName.Split(' ');
                ViewBag.firstName = names.First();
                ViewBag.lastName = names.Last();
                // The 'preferred_username' claim can be used for showing the username
                var _emailAD = userClaims?.FindFirst("preferred_username")?.Value;
                short _emailExistDB = (from _tbemp in db.tbEmpleado where _tbemp.emp_CorreoElectronico == _emailAD select _tbemp.emp_Id).FirstOrDefault();
                if (_emailExistDB < 1)
                {
                    return RedirectToAction("Create", "Empleado");
                }
                else
                {

                    tbSolicitud.tipsol_Id = _tipsol_Id;
                    tbSolicitud.emp_Id = _emailExistDB;
                    var EmpData = (from _tbemp in db.tbEmpleado where _tbemp.emp_Id == _emailExistDB select new { nombreEmpleado = _tbemp.emp_Nombres + " " + _tbemp.emp_Apellidos, emailJefe = _tbemp.emp_CorreoElectronico }).FirstOrDefault();
                    tbSolicitud.Emp_Name = EmpData.nombreEmpleado;
                    tbSolicitud.Emp_Mail = _emailAD;
                    ViewBag.EmpNames = EmpData.nombreEmpleado;
                    ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion");
                    ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
                    ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
                    ViewBag.tmo_id = new SelectList(db.tbMoneda, "tmo_id", "tmo_Abreviatura");
                    ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
                    ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
                    ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion");
                    ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");

                    return View(tbSolicitud);
                }
            }
            catch (Exception Ex)
            {
                TempData["controllerName"] = RouteData.Values["controller"];
                TempData["actionName"]= RouteData.Values["action"];
                TempData["idValue"] = _tipsol_Id;

                return RedirectToAction("Error500","Home");
            }

        }

        [HttpGet]
        public ActionResult _AccionPersonal(int meter_id)
        {
            return PartialView(meter_id);
        }

        ///BIND
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "sol_Id, emp_Id, tipsol_Id, pto_Id, tpsal_id, tmo_Id, are_Id, tipmo_id, tpv_Id, tperm_Id, sol_GralDescripcion, sol_GralJefeInmediato, sol_GralCorreoJefeInmediato, sol_GralComentario, sol_GralJustificacion, sol_GralFechaSolicitud, sol_AnviFechaViaje, sol_Anvi_Cliente, sol_Anvi_LugarDestino, sol_Acper_Anterior, sol_Anvi_PropositoVisita, sol_Anvi_DiasVisita, sol_AnviHospedaje, sol_AnviTrasladoHacia, sol_AnsolMonto, sol_PerFechaRegreso, sol_PerMedioDia, sol_PerFechaMedioDia ,sol_PerFechaInicio, sol_PerCantidadDias, sol_ReemMonto, sol_ReemFechaMonto, sol_ReemProveedor, sol_ReemCargoA, sol_ReemFechaGastos, sol_ReemNoFactura, sol_ReemMontoTotal, sol_AprRtn, sol_AprNombreEmpresa, sol_AprCiudad, sol_AprDireccion, sol_ApreTelefono, sol_ApreContactoAdm, sol_ApreCorreoAdm, sol_ApreNombreTecn, sol_ApreTelefonoTecn, sol_ApreCorreoTecn, sol_ApreCargoTecn, sol_ApreLink, sol_Acper_Nuevo, sol_RequeCantidad, sol_UsuarioCrea," +
            " sol_FechaCrea, sol_UsuarioModifica, sol_FechaModifica, Emp_Name,Emp_Mail")] tbSolicitud tbSolicitud)
        {
           
            ViewBag.emp_Id = new SelectList(db.tbEmpleado, "emp_Id", "emp_Nombres");
            ViewBag.tipsol_Id = new SelectList(db.tbTipoSolicitud, "tipsol_Id", "tipsol_Descripcion");
            ViewBag.pto_Id = new SelectList(db.tbPuesto, "pto_Id", "pto_Descripcion");
            ViewBag.tpsal_id = new SelectList(db.tbTipoSalario, "tpsal_id", "tpsal_Descripcion");
            ViewBag.tmo_id = new SelectList(db.tbMoneda, "tmo_Id", "tmo_Abreviatura");
            ViewBag.tipmo_id = new SelectList(db.tbTipoMovimiento, "tipmo_id", "tipmo_Movimiento");
            ViewBag.tpv_Id = new SelectList(db.tbTipoViatico, "tpv_Id", "tpv_Descripcion");
            ViewBag.tperm_Id = new SelectList(db.tbTipoPermiso, "tperm_Id", "tperm_Descripcion");
            ViewBag.are_Id = new SelectList(db.tbArea, "are_Id", "are_Descripcion");
            if (ModelState.IsValid)
            {
                try
                {
                    var MensajeError = "";
                    IEnumerable<object> list = null;
                    var _date = Convert.ToDateTime(tbSolicitud.sol_GralFechaSolicitud);
                    list = db.UDP_Gral_tSolicitudInsertar(tbSolicitud.emp_Id, tbSolicitud.tipsol_Id, tbSolicitud.pto_Id, tbSolicitud.tpsal_id, tbSolicitud.tmo_Id,
                    tbSolicitud.are_Id, tbSolicitud.tipmo_id, tbSolicitud.tpv_Id, tbSolicitud.tperm_Id, tbSolicitud.sol_GralDescripcion, tbSolicitud.sol_GralJefeInmediato,
                    tbSolicitud.sol_GralCorreoJefeInmediato, tbSolicitud.sol_GralComentario, tbSolicitud.sol_GralJustificacion, tbSolicitud.sol_GralFechaSolicitud,
                    tbSolicitud.sol_AnviFechaViaje, tbSolicitud.sol_Anvi_Cliente, tbSolicitud.sol_Anvi_LugarDestino, tbSolicitud.sol_Acper_Anterior,
                    tbSolicitud.sol_Anvi_PropositoVisita, tbSolicitud.sol_Anvi_DiasVisita, tbSolicitud.sol_AnviHospedaje, tbSolicitud.sol_AnviTrasladoHacia,
                    tbSolicitud.sol_AnsolMonto, tbSolicitud.sol_PerFechaRegreso, tbSolicitud.sol_PerMedioDia, tbSolicitud.sol_PerFechaMedioDia, tbSolicitud.sol_PerFechaInicio, tbSolicitud.sol_PerCantidadDias,
                    tbSolicitud.sol_ReemMonto, tbSolicitud.sol_ReemFechaMonto, tbSolicitud.sol_ReemProveedor, tbSolicitud.sol_ReemCargoA, tbSolicitud.sol_ReemFechaGastos,
                    tbSolicitud.sol_ReemNoFactura, tbSolicitud.sol_ReemMontoTotal, tbSolicitud.sol_AprRtn, tbSolicitud.sol_AprNombreEmpresa, tbSolicitud.sol_AprCiudad,
                    tbSolicitud.sol_AprDireccion, tbSolicitud.sol_ApreTelefono, tbSolicitud.sol_ApreContactoAdm, tbSolicitud.sol_ApreCorreoAdm, tbSolicitud.sol_ApreNombreTecn,
                    tbSolicitud.sol_ApreTelefonoTecn, tbSolicitud.sol_ApreCorreoTecn, tbSolicitud.sol_ApreCargoTecn, tbSolicitud.sol_ApreLink, tbSolicitud.sol_Acper_Nuevo,
                    tbSolicitud.sol_RequeCantidad, _functions.GetUser(), _functions.DatetimeNow(), _functions.GetUser(), _functions.DatetimeNow());
                    foreach (UDP_Gral_tSolicitudInsertar_Result Solicitud in list)

                        MensajeError = Solicitud.MensajeError;
                    if (MensajeError.StartsWith("-1"))
                    {

                        ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                        return View(tbSolicitud);
                    }
                    else
                    {
                        
                        string lvMensajeError = "";
                        LeerDatos(out lvMensajeError, tbSolicitud.Emp_Mail,tbSolicitud.Emp_Name);

                        TempData["smserror"] = "Solicitud Realizada con Exito.";
                        ViewBag.smserror = TempData["smserror"];


                        return RedirectToAction("Solicitud", "Menu", tbSolicitud.tipsol_Id);
                    }
                }
                catch (Exception Ex)
                {

                    ModelState.AddModelError("", "No se pudo insertar el registro, favor contacte al administrador.");
                    return View(tbSolicitud);
                }
            }
            return RedirectToAction("Solicitud", "Menu", tbSolicitud.tipsol_Id);
        }


        public int LeerDatos(out string pvMensajeError, string _Destinatario, string _DestinatarioName)
        {
            try
            {
                pvMensajeError = "";
                string lsSubject = "",
                        lsRutaPlantilla = "",
                        lsXMLDatos = "",
                        lsXMLEnvio = "";
                lsSubject = "REF:(VIA-000000001)";
                lsRutaPlantilla = @"C:\Users\PaulaDiaz\Documents\Visual Studio 2015\Projects\SIGEM_BIDSS\SIGEM_BIDSS\Content\Email\index.xml";

                lsXMLDatos = @"<principal>
                         <to>" + _Destinatario + "</to>" +
                            @"<from>Jani</from>" +
                            @"<heading>Reminder</heading>
                        <body>Don't forget me this weekend!</body>
                        </principal>";
                var _Parameters = (from _tbParm in db.tbParametro select _tbParm ).FirstOrDefault();
                EmailGenerar_Body(lsRutaPlantilla, lsXMLDatos, out lsXMLEnvio);
                enviarCorreo(_Parameters.par_Emisor, _Parameters.par_Password, lsXMLEnvio, lsSubject, _Destinatario, _Parameters.par_Servidor, _Parameters.par_Puerto);
                return 0;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        private Boolean EmailGenerar_Body(string psRutaPlantilla, string psXML, out string psXMLEnvio)
        {
            psXMLEnvio = "";
            try
            {
                //Leer
                XmlTextReader reader = new XmlTextReader(psRutaPlantilla);
                reader.Read();

                //Cargar
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(reader);

                XmlReader xmlData = XmlReader.Create(new StringReader(psXML));

                XmlWriterSettings Configuraciones = new XmlWriterSettings();
                Configuraciones.OmitXmlDeclaration = true;
                Configuraciones.ConformanceLevel = ConformanceLevel.Fragment;
                //Configuraciones.Encoding = Encoding.UTF8;
                Configuraciones.CloseOutput = false;

                //Empieza a hacer el match
                using (StringWriter sw = new StringWriter())
                using (XmlWriter xwo = XmlWriter.Create(sw, Configuraciones)) // xslt.OutputSettings use OutputSettings of xsl, so it can be output as HTML
                {
                    xslt.Transform(xmlData, null, xwo);
                    psXMLEnvio = sw.ToString();
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                psXMLEnvio = "";
                return false;
            }
        }

        public int enviarCorreo(string psEmisor, string psPassword, string psMensaje, string psAsunto, string psDestinatario, string psServidor, int psPuerto)
        {
            //      0 = Ok      //
            //      1 = Error   //

            MailMessage correos = new MailMessage();
            SmtpClient envios = new SmtpClient();

            try
            {
                correos.To.Clear();
                correos.Body = "";
                correos.Subject = "";
                correos.Body = psMensaje;
                correos.BodyEncoding = System.Text.Encoding.UTF8;
                correos.Subject = psAsunto;
                correos.IsBodyHtml = true;
                correos.To.Add(psDestinatario.Trim());
                correos.From = new MailAddress(psEmisor);
                envios.Credentials = new NetworkCredential(psEmisor, psPassword);
                // Datos del servidor //
                envios.Host = psServidor;
                envios.Port = psPuerto;
                envios.EnableSsl = true;
                //Función de envío de correo //
                envios.Send(correos);
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

    }
}