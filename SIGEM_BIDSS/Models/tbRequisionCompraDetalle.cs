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
    
    public partial class tbRequisionCompraDetalle
    {
        public int Reqde_Id { get; set; }
        public int Reqco_Id { get; set; }
        public int prod_Id { get; set; }
        public decimal Reqde_Cantidad { get; set; }
        public string Reqde_Justificacion { get; set; }
        public int Reqde_UsuarioCrea { get; set; }
        public System.DateTime Reqde_FechaCrea { get; set; }
        public Nullable<int> Reqde_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Reqde_FechaModifica { get; set; }
    
        public virtual tbRequisionCompra tbRequisionCompra { get; set; }
        public virtual tbProducto tbProducto { get; set; }
    }
}