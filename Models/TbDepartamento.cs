using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SIGEM_BIDSS.Models
{
    public partial class TbDepartamento
    {
        public TbDepartamento()
        {
            TbMunicipio = new HashSet<TbMunicipio>();
        }


        [Required(ErrorMessage = "Campo {0} requerido")]
        [Display(Name = "ID")]
        public string DepId { get; set; }

        [Display(Name = "Descripción")]
        public string DepDescripcion { get; set; }

        [Display(Name = "Creado Por")]
        public int DepUsuarioCrea { get; set; }

        [Display(Name = "Creado El")]
        public DateTime DepFechaCrea { get; set; }

        [Display(Name = "Usuario Modifico")]
        public int? DepUsuarioModifica { get; set; }

        [Display(Name = "Modificado El")]
        public DateTime? DepFechaModifica { get; set; }

        public virtual ICollection<TbMunicipio> TbMunicipio { get; set; }
    }
}
