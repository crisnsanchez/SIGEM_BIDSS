using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{

    [MetadataType(typeof(SolicitudMetaData))]
    public class SolicitudMetaData
    {
        [Display(Name = "ID Solicitud")]
        public int sol_Id { get; set; }



        [Display(Name = "ID Empleado")]
        public short emp_Id { get; set; }

        [Display(Name = "ID Tipo Solicitud")]
        public int tipsol_Id { get; set; }

        [Display(Name = "ID Puesto")]
        public int pto_Id { get; set; }

        [Display(Name = "Jefe Inmediato")]
        public string sol_JefeInmediato { get; set; }

        [Display(Name = "Correo")]
        public string sol_CorreoJefeInmediato { get; set; }

        [Display(Name = "Proposito de Visita")]
        public string sol_PropositoVisita { get; set; }

        [Display(Name = "Lugar de Destino")]
        public string sol_LugarDestino { get; set; }

        [Display(Name = "Traslado Hacia")]
        public string sol_TrasladoHacia { get; set; }

        [Display(Name = "Hospedaje")]
        public string sol_Hospedaje { get; set; }

        [Display(Name = "ID Tipo Salario")]
        public int tpsal_id { get; set; }

        [Display(Name = "Monto Solicitud")]
        public Nullable<double> sol_MontoSolicitud { get; set; }

        [Display(Name = "ID Tipo Moneda")]
        public short tmo_id { get; set; }

        [Display(Name = "ID Área")]
        public int are_Id { get; set; }

        [Display(Name = "ID Tipo Movimiento")]
        public int tipmo_id { get; set; }

        [Display(Name = "ID Tipo Viático")]
        public int tpv_Id { get; set; }

        [Display(Name = "Descripción Solicitud")]
        public string sol_GralDescripcion { get; set; }

        [Display(Name = "Justificaión Solicitud")]
        public string sol_Justificacion { get; set; }

        [Display(Name = "Solicitud Cliente")]
        public string sol_Cliente { get; set; }

        [Display(Name = "Fecha Solicitud")]
        public System.DateTime sol_FechaSolicitud { get; set; }

        [Display(Name = "Fecha Regreso")]
        public System.DateTime sol_FechaRegreso { get; set; }

        [Display(Name = "Fecha Monto")]
        public System.DateTime sol_FechaMonto { get; set; }

        [Display(Name = "Cargo")]
        public string sol_CargoA { get; set; }

        [Display(Name = "Número Factura")]
        public string sol_NoFactura { get; set; }

        [Display(Name = "Cantidad Dias")]
        public Nullable<int> sol_CantidadDias { get; set; }

        [Display(Name = "Usuario Crea")]
        public Nullable<int> sol_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public Nullable<System.DateTime> sol_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> sol_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> sol_FechaModifica { get; set; }

        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbPuesto tbPuesto { get; set; }
        public virtual tbTipoMovimiento tbTipoMovimiento { get; set; }
        public virtual tbTipoSolicitud tbTipoSolicitud { get; set; }
        public virtual tbTipoMoneda tbTipoMoneda { get; set; }
        public virtual tbTipoSalario tbTipoSalario { get; set; }
        public virtual tbTipoViatico tbTipoViatico { get; set; }
    }

    public partial class tbSolicitud
    {
        [NotMapped]
        public string Emp_Name { get; set; }
    }

}