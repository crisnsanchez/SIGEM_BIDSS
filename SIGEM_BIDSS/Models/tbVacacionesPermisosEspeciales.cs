//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGEM_BIDSS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbVacacionesPermisosEspeciales
    {
        public int VPE_Id { get; set; }
        public string VPE_Correlativo { get; set; }
        public int emp_Id { get; set; }
        public int VPE_JefeInmediato { get; set; }
        public int tperm_Id { get; set; }
        public int est_Id { get; set; }
        public System.DateTime VPE_GralFechaSolicitud { get; set; }
        public System.DateTime VPE_FechaInicio { get; set; }
        public System.DateTime VPE_FechaFin { get; set; }
        public int VPE_CantidadDias { get; set; }
        public decimal VPE_MontoSolicitado { get; set; }
        public string VPE_Comentario { get; set; }
        public string VPE_RazonRechazo { get; set; }
        public int VPE_UsuarioCrea { get; set; }
        public System.DateTime VPE_FechaCrea { get; set; }
        public Nullable<int> VPE_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> VPE_FechaModifica { get; set; }
    
        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbEmpleado tbEmpleado1 { get; set; }
        public virtual tbEstado tbEstado { get; set; }
        public virtual tbTipoPermiso tbTipoPermiso { get; set; }
    }
}
