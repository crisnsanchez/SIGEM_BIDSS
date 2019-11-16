using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(TipoSangreMetaData))]
    public class TipoSangreMetaData
    {

        [Display(Name = "ID")]
        public int tps_Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MinLength(5, ErrorMessage = "Minimo {1} caracteres")]
        public string tps_nombre { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tps_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime tps_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tps_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tps_FechaModifica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleado> tbEmpleado { get; set; }
    }

    public partial class tbTipoSangre
    {
        
    }
}