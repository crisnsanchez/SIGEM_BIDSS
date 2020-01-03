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
    
    public partial class tbEstado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEstado()
        {
            this.tbAnticipoSalario = new HashSet<tbAnticipoSalario>();
            this.tbAnticipoViatico = new HashSet<tbAnticipoViatico>();
            this.tbLiquidacionAnticipoViatico = new HashSet<tbLiquidacionAnticipoViatico>();
            this.tbEmpleado = new HashSet<tbEmpleado>();
            this.tbSolicitudReembolsoGastos = new HashSet<tbSolicitudReembolsoGastos>();
            this.tbVacacionesPermisosEspeciales = new HashSet<tbVacacionesPermisosEspeciales>();
        }
    
        public int est_Id { get; set; }
        public string est_Descripcion { get; set; }
        public int est_UsuarioCrea { get; set; }
        public System.DateTime est_FechaCrea { get; set; }
        public Nullable<int> est_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> est_FechaModifica { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbAnticipoSalario> tbAnticipoSalario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbAnticipoViatico> tbAnticipoViatico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbLiquidacionAnticipoViatico> tbLiquidacionAnticipoViatico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleado> tbEmpleado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSolicitudReembolsoGastos> tbSolicitudReembolsoGastos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbVacacionesPermisosEspeciales> tbVacacionesPermisosEspeciales { get; set; }
    }
}
