using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models
{
    [ModelMetadataType(typeof(TbTipoSangremetada))]
    public partial class TbTipoSangre
    {


    }
    public class TbTipoSangremetada
    {
        [Display(Name = "ID")]
        public int TpsId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        public string TpsNombre { get; set; }
    }
}
