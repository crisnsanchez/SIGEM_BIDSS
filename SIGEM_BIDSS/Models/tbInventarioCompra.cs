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
    
    public partial class tbInventarioCompra
    {
        public int invCom_Id { get; set; }
        public int sol_Id { get; set; }
        public string invCom_Descripcion { get; set; }
        public int invCom_UsuarioCrea { get; set; }
        public System.DateTime invCom_FechaCrea { get; set; }
        public Nullable<int> invCom_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> invCom_FechaModifica { get; set; }
    
        public virtual tbSolicitud tbSolicitud { get; set; }
    }
}
