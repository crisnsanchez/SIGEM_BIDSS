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
    
    public partial class tbSolicitudReembolsoGastos
    {
        public int Reemga_Id { get; set; }
        public string Reemga_Correlativo { get; set; }
        public int emp_Id { get; set; }
        public bool emp_EsJefe { get; set; }
        public System.DateTime Reemga_GralFechaSolicitud { get; set; }
        public System.DateTime Reemga_FechaViaje { get; set; }
        public string Reemga_Cliente { get; set; }
        public string mun_codigo { get; set; }
        public string Reemga_PropositoVisita { get; set; }
        public int Reemga_DiasVisita { get; set; }
        public string Reemga_Comentario { get; set; }
        public int est_Id { get; set; }
        public string Reemga_RazonRechazo { get; set; }
        public int Reemga_UsuarioCrea { get; set; }
        public System.DateTime Reemga_FechaCrea { get; set; }
        public int Reemga_UsuarioModifica { get; set; }
        public System.DateTime Reemga_FechaModifica { get; set; }
    
        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbEstado tbEstado { get; set; }
    }
}
