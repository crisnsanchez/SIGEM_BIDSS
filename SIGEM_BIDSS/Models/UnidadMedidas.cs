using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(UnidadMedidaMetaData))]
    public partial class tbUnidadMedida
    {

        [NotMapped]
        public List<tbUnidadMedida> tbUnidadMedidaList { get; set; }

    }
    public class UnidadMedidaMetaData
    {

        [Display(Name = "ID")]
        public int uni_Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")]
     
        public string uni_Descripcion { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Abreviatura")]
        public string uni_Abreviatura { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Usuario Crea")]

        public int uni_UsuarioCrea { get; set; }
        public System.DateTime uni_FechaCrea { get; set; }
        public Nullable<int> uni_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> uni_FechaModifica { get; set; }
        
    }

   


}