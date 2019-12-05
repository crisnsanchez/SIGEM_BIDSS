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
    
    public partial class tbEmpleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEmpleado()
        {
            this.tbHistorialdeSolicitud = new HashSet<tbHistorialdeSolicitud>();
            this.tbDeduccionInstitucionFinanciera = new HashSet<tbDeduccionInstitucionFinanciera>();
            this.tbSueldo = new HashSet<tbSueldo>();
            this.tbSueldo1 = new HashSet<tbSueldo>();
        }
    
        public int emp_Id { get; set; }
        public string emp_Nombres { get; set; }
        public string emp_Apellidos { get; set; }
        public string emp_Sexo { get; set; }
        public string emp_FechaNacimiento { get; set; }
        public string emp_Identificacion { get; set; }
        public string emp_Telefono { get; set; }
        public string emp_CorreoElectronico { get; set; }
        public bool emp_EsJefe { get; set; }
        public int tps_Id { get; set; }
        public int pto_Id { get; set; }
        public string emp_FechaIngreso { get; set; }
        public string emp_Direccion { get; set; }
        public string emp_RazonInactivacion { get; set; }
        public int est_Id { get; set; }
        public string emp_PathImage { get; set; }
        public string mun_codigo { get; set; }
        public Nullable<int> emp_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> emp_FechaCrea { get; set; }
        public Nullable<int> emp_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }
    
        public virtual tbEstado tbEstado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbHistorialdeSolicitud> tbHistorialdeSolicitud { get; set; }
        public virtual tbMunicipio tbMunicipio { get; set; }
        public virtual tbPuesto tbPuesto { get; set; }
        public virtual tbTipoSangre tbTipoSangre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbDeduccionInstitucionFinanciera> tbDeduccionInstitucionFinanciera { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldo> tbSueldo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbSueldo> tbSueldo1 { get; set; }
    }
}
