using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models
{
    [ModelMetadataType(typeof(AreaMetaData))]
    public partial class TbArea
    {
        
    }

    public class AreaMetaData
    {
        [Display(Name = "ID")]
        public int AreId { get; set; }

        [Display(Name = "Área")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "No se permiten mas de 50 Caracteres.")]
        public string AreDescripcion { get; set; }


        //public int AreUsuarioCrea { get; set; }

        //public DateTime AreFechaCrea { get; set; }

        //public int? AreUsuarioModifica { get; set; }

        //public DateTime? AreFechaModifica { get; set; }
    }
}
