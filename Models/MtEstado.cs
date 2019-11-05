using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models
{
    [ModelMetadataType(typeof(TbEstadometadata))]
    public partial class TbEstado
    {


    }
    public class TbEstadometadata
    {
        [Display(Name = "ID")]
        
        public int EstId { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "No se permiten mas de 50 Caracteres.")]
        public string EstDescripcion { get; set; }

        //public int EstUsuarioCrea { get; set; }
        //public DateTime EstFechaCrea { get; set; }
        //public int? EstUsuarioModifica { get; set; }
        //public DateTime? EstFechaModifica { get; set; }
    }
}
