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
    
    public partial class tbAnticipoViatico
    {
        public int Anvi_Id { get; set; }
        public int emp_Id { get; set; }
        public int Anvi_JefeInmediato { get; set; }
        public string Anvi_Comentario { get; set; }
        public System.DateTime Anvi_GralFechaSolicitud { get; set; }
        public System.DateTime Anvi_FechaViaje { get; set; }
        public string Anvi_Cliente { get; set; }
        public string mun_Codigo { get; set; }
        public string Anvi_PropositoVisita { get; set; }
        public int Anvi_DiasVisita { get; set; }
        public bool Anvi_Hospedaje { get; set; }
        public Nullable<int> Anvi_tptran_Id { get; set; }
        public bool Anvi_Autorizacion { get; set; }
        public int Anvi_UsuarioCrea { get; set; }
        public System.DateTime Anvi_FechaCrea { get; set; }
        public Nullable<int> Anvi_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Anvi_FechaModifica { get; set; }
    
        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbEmpleado tbEmpleado1 { get; set; }
        public virtual tbMunicipio tbMunicipio { get; set; }
        public virtual tbTipoTransporte tbTipoTransporte { get; set; }
    }
}
