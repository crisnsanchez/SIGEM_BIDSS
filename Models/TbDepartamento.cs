using System;
using System.Collections.Generic;

namespace SIGEM_BIDSS.Models
{
    public partial class TbDepartamento
    {
        public TbDepartamento()
        {
            TbMunicipio = new HashSet<TbMunicipio>();
        }

        public string DepId { get; set; }
        public string DepDescripcion { get; set; }
        public int DepUsuarioCrea { get; set; }
        public DateTime DepFechaCrea { get; set; }
        public int? DepUsuarioModifica { get; set; }
        public DateTime? DepFechaModifica { get; set; }

        public virtual ICollection<TbMunicipio> TbMunicipio { get; set; }
    }
}
