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
    
    public partial class tbPuesto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbPuesto()
        {
            this.tbHistorialdeSolicitud = new HashSet<tbHistorialdeSolicitud>();
            this.tbEmpleado = new HashSet<tbEmpleado>();
        }
    
        public int pto_Id { get; set; }
        public int are_Id { get; set; }
        public string pto_Descripcion { get; set; }
        public int pto_UsuarioCrea { get; set; }
        public System.DateTime pto_FechaCrea { get; set; }
        public Nullable<int> pto_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pto_FechaModifica { get; set; }
    
        public virtual tbArea tbArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialdeSolicitud> tbHistorialdeSolicitud { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleado> tbEmpleado { get; set; }
    }
}
