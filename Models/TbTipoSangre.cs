using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SIGEM_BIDSS.Models
{
    public partial class TbTipoSangre
    {
        public TbTipoSangre()
        {
            TbEmpleado = new HashSet<TbEmpleado>();
        }


        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "ID")]
        public int TpsId { get; set; }

        [Display(Name = "Nombre")]
        public string TpsNombre { get; set; }

        [Display(Name = "Creado Por")]
        public int TpsUsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        public DateTime TpsFechaCrea { get; set; }

        [Display(Name = "Modificado Por")]
        public int? TpsUsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        public DateTime? TpsFechaModifica { get; set; }

        public virtual ICollection<TbEmpleado> TbEmpleado { get; set; }
    }
}
