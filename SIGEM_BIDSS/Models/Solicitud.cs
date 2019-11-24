using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{  [MetadataType(typeof(SolicitudMetaData))]
    public class SolicitudMetaData
    {

        [Display(Name = "ID SOLICITUD")]
        public int sol_Id { get; set; }

        [Display(Name = "EMPLEADO")]
        public short emp_Id { get; set; }

        [Display(Name = "TIPO SOLICITUD")]
        public int tipsol_Id { get; set; }

        [Display(Name = "PUESTO")]
        public int pto_Id { get; set; }

        [Display(Name = "ID TIPO SALARIO")]
        public int tpsal_id { get; set; }

        [Display(Name = "ID TIPO MONEDA")]
        public short tmo_Id { get; set; }

        [Display(Name = "ID AREA")]
        public int are_Id { get; set; }

        [Display(Name = "ID TIPO MOVIMIENTO")]
        public int tipmo_id { get; set; }

        [Display(Name = "ID TIPO VIATICO")]
        public int tpv_Id { get; set; }

        [Display(Name = "ID TIPO PERMISO")]
        public int tperm_Id { get; set; }

        [Display(Name = "DESCRIPCION GENERAL")]
        public string sol_GralDescripcion { get; set; }

        [Display(Name = "JEFE INMEDIATO")]
        public string sol_GralJefeInmediato { get; set; }

        [Display(Name = "CORREO JEFE INMEDIATO")]
        public string sol_GralCorreoJefeInmediato { get; set; }

        [Display(Name = "COMENTARIO GENERAL")]
        public string sol_GralComentario { get; set; }

        [Display(Name = "JUSTIFICACION GENERAL")]
        public string sol_GralJustificacion { get; set; }

        [Display(Name = "FECHA SOLICITUD")]
        public System.DateTime sol_GralFechaSolicitud { get; set; }

        [Display(Name = "FECHA VIAJE")]
        public System.DateTime sol_AnviFechaViaje { get; set; }

        [Display(Name = "CLIENTE")]
        public string sol_Anvi_Cliente { get; set; }

        [Display(Name = "LUGAR DE DESTINO")]
        public string sol_Anvi_LugarDestino { get; set; }

        [Display(Name = "PUESTO ANTERIOR")]
        public string sol_Acper_Anterior { get; set; }

        [Display(Name = "PROPOSITO VISITA")]
        public string sol_Anvi_PropositoVisita { get; set; }

        [Display(Name = "DIAS VISITA")]
        public int sol_Anvi_DiasVisita { get; set; }

        [Display(Name = "HOSPEDAJE")]
        public string sol_AnviHospedaje { get; set; }

        [Display(Name = "TRASLADO HACIA")]
        public string sol_AnviTrasladoHacia { get; set; }

        [Display(Name = "MONTO")]
        public double sol_AnsolMonto { get; set; }

        [Display(Name = "FECHA REGRESO")]
        public System.DateTime sol_PerFechaRegreso { get; set; }

        [Display(Name = "PERMISO MEDIO DIA")]
        public bool sol_PerMedioDia { get; set; }

        [Display(Name = "FECHA INICIO PERMISO")]
        public System.DateTime sol_PerFechaInicio { get; set; }

        [Display(Name = "CANTIDAD DIAS")]
        public int sol_PerCantidadDias { get; set; }

        [Display(Name = "MONTO REEMBOLSO")]
        public double sol_ReemMonto { get; set; }

        [Display(Name = "FECHA MONTO")]
        public System.DateTime sol_ReemFechaMonto { get; set; }

        [Display(Name = "PROVEEDOR REEMBOLSO")]
        public string sol_ReemProveedor { get; set; }

        [Display(Name = "CARGO")]
        public string sol_ReemCargoA { get; set; }

        [Display(Name = "FECHA GASTOS REEMBOLSOS")]
        public System.DateTime sol_ReemFechaGastos { get; set; }

        [Display(Name = "No FACTURA REEMBOLSO")]
        public string sol_ReemNoFactura { get; set; }

        [Display(Name = "TOTAL REEMBOLSO")]
        public double sol_ReemMontoTotal { get; set; }

        [Display(Name = "RTN")]
        public string sol_AprRtn { get; set; }

        [Display(Name = "NOMBRE EMPRESA")]
        public string sol_AprNombreEmpresa { get; set; }

        [Display(Name = "CUIDAD")]
        public string sol_AprCiudad { get; set; }

        [Display(Name = "DIRECCION")]
        public string sol_AprDireccion { get; set; }

        [Display(Name = "TELEFONO")]
        public string sol_ApreTelefono { get; set; }

        [Display(Name = "CONTACTO ADMINISTRACIÓN")]
        public string sol_ApreContactoAdm { get; set; }

        [Display(Name = "CORREO ADMINISTRACIÓN")]
        public string sol_ApreCorreoAdm { get; set; }

        [Display(Name = "NOMBRE TECNICO")]
        public string sol_ApreNombreTecn { get; set; }

        [Display(Name = "TELÉFONO TECNICO")]
        public string sol_ApreTelefonoTecn { get; set; }

        [Display(Name = "cORREO TECNICO")]
        public string sol_ApreCorreoTecn { get; set; }

        [Display(Name = "CARGO TECNICO")]
        public string sol_ApreCargoTecn { get; set; }

        [Display(Name = "LINK")]
        public string sol_ApreLink { get; set; }

        [Display(Name = "NUEVO")]
        public string sol_Acper_Nuevo { get; set; }

        [Display(Name = "CANTIDAD")]
        public double sol_RequeCantidad { get; set; }

        [Display(Name = "USUARIO CREA")]
        public int sol_UsuarioCrea { get; set; }

        [Display(Name = "FECHA CREA")]
        public System.DateTime sol_FechaCrea { get; set; }

        [Display(Name = "USUARIO MODIFICA")]
        public Nullable<int> sol_UsuarioModifica { get; set; }

        [Display(Name = "FECHA MODIFICA")]
        public Nullable<System.DateTime> sol_FechaModifica { get; set; }

        public virtual tbArea tbArea { get; set; }
        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbMoneda tbMoneda { get; set; }
        public virtual tbPuesto tbPuesto { get; set; }
        public virtual tbTipoMovimiento tbTipoMovimiento { get; set; }
        public virtual tbTipoPermiso tbTipoPermiso { get; set; }
        public virtual tbTipoSalario tbTipoSalario { get; set; }
        public virtual tbTipoSolicitud tbTipoSolicitud { get; set; }
        public virtual tbTipoViatico tbTipoViatico { get; set; }
    }


    public partial class tbSolicitud
    {
        [NotMapped]
        [Display(Name = "EMPLEADO")]
        public string Emp_Name { get; set; }
        
    }
}