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
    
    public partial class tbMunicipio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbMunicipio()
        {
            this.tbAnticipoViatico = new HashSet<tbAnticipoViatico>();
            this.tbProveedor = new HashSet<tbProveedor>();
            this.tbEmpleado = new HashSet<tbEmpleado>();
        }
    
        public string mun_codigo { get; set; }
        public string dep_codigo { get; set; }
        public string mun_nombre { get; set; }
        public int mun_UsuarioCrea { get; set; }
        public System.DateTime mun_FechaCrea { get; set; }
        public Nullable<int> mun_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> mun_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbAnticipoViatico> tbAnticipoViatico { get; set; }
        public virtual tbDepartamento tbDepartamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbProveedor> tbProveedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleado> tbEmpleado { get; set; }
    }
}
