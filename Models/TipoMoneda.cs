using System;
using System.Collections.Generic;

namespace SIGEM_BIDSS.Models
{
    public partial class TipoMoneda
    {
        public short TmoId { get; set; }
        public string TmoAbreviatura { get; set; }
        public string TmoNombre { get; set; }
        public int TmoUsuarioCrea { get; set; }
        public DateTime TmoFechaCrea { get; set; }
        public int? TmoUsuarioModifica { get; set; }
        public DateTime? TmoFechaModifica { get; set; }
    }
}
