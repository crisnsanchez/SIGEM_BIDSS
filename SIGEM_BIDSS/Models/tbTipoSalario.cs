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
    
    public partial class tbTipoSalario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbTipoSalario()
        {
            this.tbSolicitud = new HashSet<tbSolicitud>();
            this.tbHistorialdeSolicitud = new HashSet<tbHistorialdeSolicitud>();
        }
    
        public int tpsal_id { get; set; }
        public string tpsal_Descripcion { get; set; }
        public int tpsal_UsuarioCrea { get; set; }
        public System.DateTime tpsal_FechaCrea { get; set; }
        public Nullable<int> tpsal_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tpsal_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSolicitud> tbSolicitud { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialdeSolicitud> tbHistorialdeSolicitud { get; set; }
    }
}
