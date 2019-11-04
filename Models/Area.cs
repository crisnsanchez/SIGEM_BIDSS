using System;
using System.Collections.Generic;

namespace SIGEM_BIDSS.Models
{
    public partial class TbArea
    {
        public TbArea()
        {
            TbPuesto = new HashSet<Puesto>();
        }

        public int AreId { get; set; }
        public string AreDescripcion { get; set; }
        public int AreUsuarioCrea { get; set; }
        public DateTime AreFechaCrea { get; set; }
        public int? AreUsuarioModifica { get; set; }
        public DateTime? AreFechaModifica { get; set; }

        public virtual ICollection<Puesto> TbPuesto { get; set; }
    }
}
