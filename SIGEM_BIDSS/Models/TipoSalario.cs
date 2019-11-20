using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{

    [MetadataType(typeof(TipoSalarioMetaData))]
    public partial class tbTipoSalario
    {
        [NotMapped]
        public List<tbTipoSalario> TipoSalarioList { get; set; }
    }

    public class TipoSalarioMetaData
    {
        [Display(Name = "Id Tipo Salario")]
        public int tpsal_id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripcion")]
        [MinLength(5, ErrorMessage = "Minimo {1} caracteres")]
        public string tpsal_Descripcion { get; set; }



        [Display(Name = "Usuario Crea")]
        public int tpsal_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime tpsal_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tpsal_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tpsal_FechaModifica { get; set; }

     
        public virtual ICollection<tbSolicitud> tbSolicitud { get; set; }
    }
}