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
    
    public partial class tbTipoMovimiento
    {
        public int tipmo_id { get; set; }
        public string tipmo_Movimiento { get; set; }
        public int tipmo_UsuarioCrea { get; set; }
        public System.DateTime tipmo_FechaCrea { get; set; }
        public Nullable<int> tipmo_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tipmo_FechaModifica { get; set; }
    }
}
