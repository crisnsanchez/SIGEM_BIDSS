using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{  [MetadataType(typeof(SolicitudMetaData))]



    public partial class tbSolicitud
    {
        [NotMapped]
        [Display(Name = "Nombre Empleado")]
        public string Emp_Name { get; set; }

        [NotMapped]
        public string Emp_Mail { get; set; }

    }
    public class SolicitudMetaData
    {

        [Display(Name = "Id Solicitud")]
        public int sol_Id { get; set; }

        [Display(Name = "Empleado")]
        public short emp_Id { get; set; }

        [Display(Name = "Tipo Solicitud")]
        public int tipsol_Id { get; set; }

        [Display(Name = "Cargo")]
        public int pto_Id { get; set; }

        [Display(Name = "Tipo Salario")]
        public int tpsal_id { get; set; }

        [Display(Name = "Moneda")]
        public short tmo_Id { get; set; }

        [Display(Name = "Area")]
        public int are_Id { get; set; }

        [Display(Name = "Tipo Movimiento")]
        public int tipmo_id { get; set; }

        [Display(Name = "Tipo Viatico")]
        public int tpv_Id { get; set; }

        [Display(Name = "Tipo Permiso")]
        public int tperm_Id { get; set; }

        [Display(Name = "Descripcion")]
        public string sol_GralDescripcion { get; set; }

        [Display(Name = "Jefe Inmediato")]
        public string sol_GralJefeInmediato { get; set; }

        [Display(Name = "Correo Jefe Inmediato")]
        public string sol_GralCorreoJefeInmediato { get; set; }

        [Display(Name = "Comentario General")]
        public string sol_GralComentario { get; set; }

        [Display(Name = "Justificacion")]
        public string sol_GralJustificacion { get; set; }

        [Display(Name = "Fecha Solicitud")]
        public System.DateTime sol_GralFechaSolicitud { get; set; }

        [Display(Name = "Fecha Viaje")]
        public System.DateTime sol_AnviFechaViaje { get; set; }

        [Display(Name = "Cliente")]
        public string sol_Anvi_Cliente { get; set; }

        [Display(Name = "Lugar Destino")]
        public string sol_Anvi_LugarDestino { get; set; }

        [Display(Name = "Puesto Anterior")]
        public string sol_Acper_Anterior { get; set; }

        [Display(Name = "Proposito Visita")]
        public string sol_Anvi_PropositoVisita { get; set; }

        [Display(Name = "Dias Visita")]
        public int sol_Anvi_DiasVisita { get; set; }

        [Display(Name = "Hospedaje")]
        public string sol_AnviHospedaje { get; set; }

        [Display(Name = "Traslado Hacia")]
        public string sol_AnviTrasladoHacia { get; set; }

        [Display(Name = "Monto")]
        public double sol_AnsolMonto { get; set; }

        [Display(Name = "Fecha Regreso")]
        public System.DateTime sol_PerFechaRegreso { get; set; }

        [Display(Name = "Permiso Medio Dia")]
        public bool sol_PerMedioDia { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public System.DateTime sol_PerFechaInicio { get; set; }

        [Display(Name = "Cantidad Dias")]
        public int sol_PerCantidadDias { get; set; }

        [Display(Name = "Monto Rembolso")]
        public double sol_ReemMonto { get; set; }

        [Display(Name = "Fecha Monto")]
        public System.DateTime sol_ReemFechaMonto { get; set; }

        [Display(Name = "Proveedor")]
        public string sol_ReemProveedor { get; set; }

        [Display(Name = "Cargo")]
        public string sol_ReemCargoA { get; set; }

        [Display(Name = "Fecha Gastos")]
        public System.DateTime sol_ReemFechaGastos { get; set; }

        [Display(Name = "No Factura Rembolso")]
        public string sol_ReemNoFactura { get; set; }

        [Display(Name = "Total Rembolso")]
        public double sol_ReemMontoTotal { get; set; }

        [Display(Name = "RTN")]
        public string sol_AprRtn { get; set; }

        [Display(Name = "Nombre Empresa")]
        public string sol_AprNombreEmpresa { get; set; }

        [Display(Name = "Ciudad")]
        public string sol_AprCiudad { get; set; }

        [Display(Name = "Dirección")]
        public string sol_AprDireccion { get; set; }

        [Display(Name = "Telefóno")]
        public string sol_ApreTelefono { get; set; }

        [Display(Name = "Contacto Administración")]
        public string sol_ApreContactoAdm { get; set; }

        [Display(Name = "Correo Administración")]
        public string sol_ApreCorreoAdm { get; set; }

        [Display(Name = "Nombre Tecnico")]
        public string sol_ApreNombreTecn { get; set; }

        [Display(Name = "Teléfono Tecnico")]
        public string sol_ApreTelefonoTecn { get; set; }

        [Display(Name = "Correo Tecnico")]
        public string sol_ApreCorreoTecn { get; set; }

        [Display(Name = "Cargo Tecnico")]
        public string sol_ApreCargoTecn { get; set; }

        [Display(Name = "LINK")]
        public string sol_ApreLink { get; set; }

        [Display(Name = "Nuevo")]
        public string sol_Acper_Nuevo { get; set; }

        [Display(Name = "Cantidad")]
        public double sol_RequeCantidad { get; set; }

        [Display(Name = "Usuario")]
        public int sol_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime sol_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> sol_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
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


 
}