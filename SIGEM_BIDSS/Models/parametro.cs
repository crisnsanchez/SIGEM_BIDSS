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
       
        public string par_NombreEmpresa { get; set; }

        [Display(Name = "Telefono de la Empresa")]
        
        public string par_TelefonoEmpresa { get; set; }

        [Display(Name = "Correo de la Empresa")]
       
        public string par_CorreoEmpresa { get; set; }

        [Display(Name = "Correo Emisor")]
      
        public string par_CorreoEmisor { get; set; }


        [Display(Name = "Password")]
       
        public string par_Password { get; set; }

        [Display(Name = "Servidor")]
       
        public string par_Servidor { get; set; }

        [Display(Name = "Puerto")]
       
        public int par_Puerto { get; set; }

        [Display(Name = "Logo")]
      
        public string par_PathLogo { get; set; }
    }
}