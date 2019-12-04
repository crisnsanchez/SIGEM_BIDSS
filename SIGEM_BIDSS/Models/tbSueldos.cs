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
    
    public partial class tbSueldos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbSueldos()
        {
            this.tbSueldos1 = new HashSet<tbSueldos>();
            this.tbSueldos11 = new HashSet<tbSueldos>();
        }
    
        public int sue_Id { get; set; }
        public short emp_Id { get; set; }
        public short tmo_Id { get; set; }
        public Nullable<decimal> sue_Cantidad { get; set; }
        public Nullable<int> sue_SueldoAnterior { get; set; }
        public bool sue_Estado { get; set; }
        public string sue_RazonInactivo { get; set; }
        public int sue_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> sue_FechaCrea { get; set; }
        public Nullable<int> ue_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> sue_FechaModifica { get; set; }
    
        public virtual tbMoneda tbMoneda { get; set; }
        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbEmpleado tbEmpleado1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldos> tbSueldos1 { get; set; }
        public virtual tbSueldos tbSueldos2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldos> tbSueldos11 { get; set; }
        public virtual tbSueldos tbSueldos3 { get; set; }
    }
}
