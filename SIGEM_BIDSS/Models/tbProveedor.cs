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
    
    public partial class tbProveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbProveedor()
        {
            this.tbProducto = new HashSet<tbProducto>();
        }
    
        public int prov_Id { get; set; }
        public string prov_Nombre { get; set; }
        public string prov_NombreContacto { get; set; }
        public string prov_Direccion { get; set; }
        public string mun_codigo { get; set; }
        public string prov_Email { get; set; }
        public string prov_Telefono { get; set; }
        public string prov_RTN { get; set; }
        public short acte_Id { get; set; }
        public int prov_UsuarioCrea { get; set; }
        public System.DateTime prov_FechaCrea { get; set; }
        public Nullable<int> prov_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> prov_FechaModifica { get; set; }
    
        public virtual tbActividadEconomica tbActividadEconomica { get; set; }
        public virtual tbMunicipio tbMunicipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbProducto> tbProducto { get; set; }
    }
}
