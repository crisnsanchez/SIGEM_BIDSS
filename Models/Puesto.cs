using System;
using System.Collections.Generic;

namespace SIGEM_BIDSS.Models
{
    public partial class Puesto
    {
        public Puesto()
        {
            TbEmpleado = new HashSet<TbEmpleado>();
        }

        public int PtoId { get; set; }
        public int AreId { get; set; }
        public string PtoDescripcion { get; set; }
        public int PtoUsuarioCrea { get; set; }
        public DateTime PtoFechaCrea { get; set; }
        public int? PtoUsuarioModifica { get; set; }
        public DateTime? PtoFechaModifica { get; set; }

        public virtual TbArea Are { get; set; }
        public virtual ICollection<TbEmpleado> TbEmpleado { get; set; }
    }
}
