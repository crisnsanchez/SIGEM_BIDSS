using System;
using System.Collections.Generic;

namespace SIGEM_BIDSS.Models
{
    public partial class TipoSolicitud
    {
        public int TipsolId { get; set; }
        public string TipsolDescripcion { get; set; }
        public int TipsolUsuarioCrea { get; set; }
        public DateTime TipsolFechaCrea { get; set; }
        public int? TipsolUsuarioModifica { get; set; }
        public DateTime? TipsolFechaModifica { get; set; }
    }
}
