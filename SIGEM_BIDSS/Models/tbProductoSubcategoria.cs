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
    
    public partial class tbProductoSubcategoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbProductoSubcategoria()
        {
            this.tbProducto = new HashSet<tbProducto>();
        }
    
        public int pscat_Id { get; set; }
        public string pscat_Descripcion { get; set; }
        public int pcat_Id { get; set; }
        public bool pscat_EsActiva { get; set; }
        public int pscat_UsuarioCrea { get; set; }
        public System.DateTime pscat_FechaCrea { get; set; }
        public Nullable<int> pscat_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pscat_FechaModifica { get; set; }
    
        public virtual tbProductoCategoria tbProductoCategoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbProducto> tbProducto { get; set; }
    }
}
