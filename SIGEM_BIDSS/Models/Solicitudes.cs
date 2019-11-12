using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(SolicitudMetaData))]


    
    public partial class tbSolicitud
    {
        [NotMapped]
        public List<tbSolicitud> solicitudList { get; set; }
    }

    public class SolicitudMetaData
    {
        public int sol_Id { get; set; }
        public string sol_Descripcion { get; set; }
        public short emp_Id { get; set; }
        public int tipsol_Id { get; set; }
        public Nullable<int> pto_Id { get; set; }
        public string sol_JefeInmediato { get; set; }
        public string sol_CorreoJefeInmediato { get; set; }
        public string sol_Cliente { get; set; }
        public System.DateTime sol_FechaSolicitud { get; set; }
        public System.DateTime sol_FechaRegreso { get; set; }
        public Nullable<int> sol_CantidadDias { get; set; }
        public int tpv_Id { get; set; }
        public Nullable<int> sol_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> sol_FechaCrea { get; set; }
        public Nullable<int> sol_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> sol_FechaModifica { get; set; }

        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbPuesto tbPuesto { get; set; }
        public virtual tbTipoSolicitud tbTipoSolicitud { get; set; }
        public virtual tbTipoViatico tbTipoViatico { get; set; }
    }
}