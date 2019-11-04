using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SIGEM_BIDSS.Models
{
    public partial class TbPuesto
    {
        public TbPuesto()
        {
            TbEmpleado = new HashSet<TbEmpleado>();
        }
        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "ID")]
        public int PtoId { get; set; }

        [Display(Name = "ID Area")]
        public int AreId { get; set; }

        [Display(Name = "Descripción")]
        public string PtoDescripcion { get; set; }

        [Display(Name = "Creado Por")]
        public int PtoUsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        public DateTime PtoFechaCrea { get; set; }

        [Display(Name = "Mkodificado Por")]
        public int? PtoUsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        public DateTime? PtoFechaModifica { get; set; }

        public virtual TbArea Are { get; set; }
        public virtual ICollection<TbEmpleado> TbEmpleado { get; set; }
    }
}
