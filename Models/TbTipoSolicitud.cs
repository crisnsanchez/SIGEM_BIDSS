using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SIGEM_BIDSS.Models
{
    public partial class TbTipoSolicitud
    {

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "ID")]
        public int TipsolId { get; set; }

        [Display(Name = "Descripción")]
        public string TipsolDescripcion { get; set; }

        [Display(Name = "Creado Por")]
        public int TipsolUsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        public DateTime TipsolFechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public int? TipsolUsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        public DateTime? TipsolFechaModifica { get; set; }
    }
}
