using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIGEM_BIDSS.Models
{
    public partial class TbArea
    {
        public TbArea()
        {
            TbPuesto = new HashSet<TbPuesto>();
        }

        [Display(Name = "ID")]
        public int AreId { get; set; }

        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "Descripcion")]
        public string AreDescripcion { get; set; }
        [Display(Name = "Creado Por")]
        public int AreUsuarioCrea { get; set; }
        [Display(Name = "Creado El")]
        public DateTime AreFechaCrea { get; set; }
         [Display(Name = "Modificado Por")]
        public int? AreUsuarioModifica { get; set; }
        [Display(Name = "Modificado El")]
        public DateTime? AreFechaModifica { get; set; }

        public virtual ICollection<TbPuesto> TbPuesto { get; set; }
    }
}
