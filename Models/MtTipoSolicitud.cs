using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models
{
    [ModelMetadataType(typeof(TbTipoSolicitudmetadata))]
    public partial class TbTipoSolicitud
    {


    }
    public class TbTipoSolicitudmetadata
    {
        [Display(Name = "ID")]
        public int TipsolId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Descripción")]
        public string TipsolDescripcion { get; set; }
    }
}
