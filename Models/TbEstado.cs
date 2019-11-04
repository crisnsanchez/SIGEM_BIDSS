using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIGEM_BIDSS.Models
{
    public partial class TbEstado
    {

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "ID")]
        public int EstId { get; set; }

        [Display(Name = "Descripcion")]
        public string EstDescripcion { get; set; }

        [Display(Name = "Creado Por")]
        public int EstUsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        public DateTime EstFechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public int? EstUsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        public DateTime? EstFechaModifica { get; set; }
    }
}
