using System;
using System.Collections.Generic;

namespace SIGEM_BIDSS.Models
{
    public partial class TbTipoSangre
    {
        public TbTipoSangre()
        {
            TbEmpleado = new HashSet<TbEmpleado>();
        }

        public int TpsId { get; set; }
        public string TpsNombre { get; set; }
        public int TpsUsuarioCrea { get; set; }
        public DateTime TpsFechaCrea { get; set; }
        public int? TpsUsuarioModifica { get; set; }
        public DateTime? TpsFechaModifica { get; set; }

        public virtual ICollection<TbEmpleado> TbEmpleado { get; set; }
    }
}
