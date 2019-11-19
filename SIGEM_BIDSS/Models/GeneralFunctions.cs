using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public int tipsol_Id(int _TipoSolicitud)
        {
            int tipsol = db.tbTipoSolicitud.Find(_TipoSolicitud).tipsol_Id;
            return tipsol;
        }
        public object TipodeSolicitud()
        {
            object obtipsol = new
            {
                AccionPersonal = TipodeSolicitudSearch("Accion Personal"),
                AnticipodeViaticos = TipodeSolicitudSearch("Anticipo de Viaticos")
            };
            return obtipsol;
        }

        public int TipodeSolicitudSearch(string _strtiposolSearch)
        {
            if (_strtiposolSearch == null || _strtiposolSearch == "") { return 0; }
            int tipsol = (from _dbtts in db.tbTipoSolicitud where (_dbtts.tipsol_Descripcion == _strtiposolSearch) select (_dbtts.tipsol_Id)).FirstOrDefault();
            return tipsol;
        }


        //public const string AccionPersonal = "Accion Personal";

        public const int AccionPersonal = 1;

        public const int AnticipodeViaticos = 2;
    



        public const int empleadoinactivo = 2;
        public const int empleadoactivo = 1;
    }


}