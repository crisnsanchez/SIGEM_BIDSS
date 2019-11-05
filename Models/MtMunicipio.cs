using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models
{
    [ModelMetadataType(typeof(TbMunicipiometada))]
    public partial class TbMunicipio
    {


    }

    public class TbMunicipiometada
    {
        [Display(Name = "Código Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string MunId { get; set; }

        [Display(Name = "Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string DepId { get; set; }



        [Display(Name = "Municipio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string MunNombre { get; set; }

        //public int MunUsuarioCrea { get; set; }
        //public DateTime MunFechaCrea { get; set; }
        //public int? MunUsuarioModifica { get; set; }
        //public DateTime? MunFechaModifica { get; set; }
    }
}
