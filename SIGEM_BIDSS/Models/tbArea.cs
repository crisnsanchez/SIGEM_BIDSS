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
    
    public partial class tbArea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbArea()
        {
            this.tbPuesto = new HashSet<tbPuesto>();
            this.tbRequisionCompra = new HashSet<tbRequisionCompra>();
        }
    
        public int are_Id { get; set; }
        public string are_Descripcion { get; set; }
        public int are_UsuarioCrea { get; set; }
        public System.DateTime are_FechaCrea { get; set; }
        public Nullable<int> are_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> are_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPuesto> tbPuesto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbRequisionCompra> tbRequisionCompra { get; set; }
    }
}
