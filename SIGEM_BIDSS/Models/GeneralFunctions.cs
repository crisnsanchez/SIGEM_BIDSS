using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace SIGEM_BIDSS.Models
{
    public class GeneralFunctions
    {

        private SIGEM_BIDSSEntities db = new SIGEM_BIDSSEntities();
        public List<tbEmpleado> Sexo()
        {
            List<tbEmpleado> Hola = new List<tbEmpleado>();
            Hola.Add(new tbEmpleado { ge_Id = "", ge_Description = "Seleccione Uno" });
            Hola.Add(new tbEmpleado { ge_Id = "F", ge_Description = "Femenino" });
            Hola.Add(new tbEmpleado { ge_Id = "M", ge_Description = "Masculino" });
            return Hola;
        }
        
        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }
        public int GetUser()
        {
            int _user = 1;
            return _user;
        }
      
        public const string _isCreated = "Created";
        public const string _isEdited = "Edited";



        public const string Pass = "QdZwAxesc12";


        public const int intDefault = 1;
        public const int tiposalario = 1;
        public const int tiposoli = 2;


        public const string stringDefault = "-----";
        public const string dateDefault = "01/01/1900";
        public const string date2 = "1900/01/01";
        public const bool boolDefault = false;


        //public const string AccionPersonal = "Accion Personal";
        public const int AccionPersonal = 1;
        public const int SolicitudPermisoLaboral = 2;
        public const int SolicitudAnticipoViaticos = 3;
        public const int SolicitudAnticipoSalario = 4;
        public const int LiquidacionAnticipoSalario = 5;
        public const int RequisicionCompra = 6;
        public const int RembolsoGastos = 7;
        public const int empleadoinactivo = 2;
        public const int empleadoactivo = 1;
        public const bool instfinDenegar = false;
        public const bool instfinAceptar = true;

        public int GetUser(out string UserName)
        {
            string email = "";
            int _user = 0;

            UserName = ClaimsPrincipal.Current.FindFirst("name").Value;
            email = ClaimsPrincipal.Current.FindFirst("preferred_username").Value;

            var Usuario = db.tbEmpleado.Select(s => new {
                emp_Id = s.emp_Id,
                emp_CorreoElectronico = s.emp_CorreoElectronico
            }).Where(x => x.emp_CorreoElectronico == email).ToList();

            foreach (var Result in Usuario)
                _user = Result.emp_Id;

            return _user;
        }

        //Call
        //string lvMensajeError = "";
        //LeerDatos(out lvMensajeError, tbSolicitud.sol_GralCorreoJefeInmediato, tbSolicitud.sol_GralJefeInmediato, tbSolicitud.Emp_Name);

        public int LeerDatosSol(out string pvMensajeError, string _empJefeMail, string _GralJefeInmediato, string _empName)
        {
            try
            {
                pvMensajeError = "";
                string lsSubject = "",
                        lsRutaPlantilla = "",
                        lsXMLDatos = "",
                        lsXMLEnvio = "";
                lsSubject = "REF:(VIA-000000001)";
                lsRutaPlantilla = @"C:\GitHub\SIGEM_BIDSS\SIGEM_BIDSS\Content\Email\index.xml";

                lsXMLDatos = @"<principal>
                              <nref>REF:(VIA-000000001)</nref>
                              <to>" + _GralJefeInmediato + "</to>" +
                              @"<from>" + _empName + "</from>" +
                              "</principal>";

                var _Parameters = (from _tbParm in db.tbParametro select _tbParm).FirstOrDefault();
                EmailGenerar_BodySol(lsRutaPlantilla, lsXMLDatos, out lsXMLEnvio);
                enviarCorreoSol(_Parameters.par_CorreoEmisor, _Parameters.par_Password, lsXMLEnvio, lsSubject, _empJefeMail, _Parameters.par_Servidor, _Parameters.par_Puerto);
                return 0;
            }
            catch (Exception Ex)
            {

                throw;
            }
        }

        private Boolean EmailGenerar_BodySol(string psRutaPlantilla, string psXML, out string psXMLEnvio)
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

        public int enviarCorreoSol(string psEmisor, string psPassword, string psMensaje, string psAsunto, string psDestinatario, string psServidor, int psPuerto)
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


        //Estados Solicitud
        public const int Enviada = 1;
        public const int Revisada = 2;
        public const int Aprobada = 3;
        public const int Rechazada = 4;

        //Bitácora de Errores
        public void BitacoraErrores(string Controller, string Action, string User, string ErrorMessage)
        {
            db.UDP_Acce_tbBitacoraErrores_Insert(Controller, Action, User, DatetimeNow(), ErrorMessage);
        }

        public bool LeerDatos(out string pvMensajeError, string Reference)
        {
            pvMensajeError = "";
            string UserName = "",
                    EmailDesti = "",
                    lsSubject = "",
                    lsRutaPlantilla = "",
                    lsXMLDatos = "",
                    lsXMLEnvio = "";

            bool State = false;
            int StateIn = 0;

            EmailDesti = ClaimsPrincipal.Current.FindFirst("preferred_username").Value;

            GetUser(out UserName);
            lsSubject = Reference;

            lsRutaPlantilla = System.AppContext.BaseDirectory + "Content\\Email\\AnticipoViatico.xml";

            lsXMLDatos = @"<principal>
                              <UserFullName>" + UserName + "</UserFullName>"
                         + "</principal>";

            State = EmailGenerar_Body(lsRutaPlantilla, lsXMLDatos, out lsXMLEnvio, out pvMensajeError);
            if (State)
            {
                //Si todo está bien procedo a enviar correo
                StateIn = enviarCorreo("magdaly2511@gmail.com", "Sinceridad251123", lsXMLEnvio, lsSubject, EmailDesti, "smtp.gmail.com", "25", out pvMensajeError);
                if (StateIn == 1)
                    BitacoraErrores("GeneralFunctions", "enviarCorreo", UserName, pvMensajeError);

                return true;
            }
            else
            {
                BitacoraErrores("GeneralFunctions", "EmailGenerar_Body", UserName, pvMensajeError);
                return false;
            }

        }

        private bool EmailGenerar_Body(string psRutaPlantilla, string psXML, out string psXMLEnvio, out string ErrorMessage)
        {
            psXMLEnvio = "";
            ErrorMessage = "";
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
                ErrorMessage = ex.Message.ToString();
                psXMLEnvio = "";
                return false;
            }
        }

        public int enviarCorreo(string psEmisor, string psPassword, string psMensaje, string psAsunto, string psDestinatario, string psServidor, string psPuerto, out string ErrorMessage)
        {
            //      0 = Ok      //
            //      1 = Error   //
            ErrorMessage = "";
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
                envios.Port = Convert.ToInt32(psPuerto);
                envios.EnableSsl = true;
                //Función de envío de correo //
                envios.Send(correos);
                return 0;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return 1;
            }
        }
    }


}