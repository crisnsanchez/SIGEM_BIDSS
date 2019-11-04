using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SIGEM_BIDSS.Models
{
    public partial class TbTipoMoneda
    {

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "ID")]
        public short TmoId { get; set; }

        [Display(Name = "Descripcion")]
        public string TmoAbreviatura { get; set; }

        [Display(Name = "Nombre")]
        public string TmoNombre { get; set; }

        [Display(Name = "Creado Por")]
        public int TmoUsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        public DateTime TmoFechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public int? TmoUsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        public DateTime? TmoFechaModifica { get; set; }
    }
}
