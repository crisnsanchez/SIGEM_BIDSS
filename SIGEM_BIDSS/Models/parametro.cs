using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(ParametrosMetaData))]
    public partial class tbParametro
    {
    }
    public class ParametrosMetaData
    {

        [Display(Name = "Nombre Empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_NombreEmpresa { get; set; }

        [Display(Name = "Telefono de la Empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_TelefonoEmpresa { get; set; }

        [Display(Name = "Correo de la Empresa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_CorreoEmpresa { get; set; }

        [Display(Name = "Correo Emisor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_CorreoEmisor { get; set; }

        [Display(Name = "Correo RRHH")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_CorreoRRHH { get; set; }


        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_Password { get; set; }

        [Display(Name = "Servidor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string par_Servidor { get; set; }

        [Display(Name = "Puerto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int par_Puerto { get; set; }

        [Display(Name = "Logo")]
      
        public string par_PathLogo { get; set; }
    }
}