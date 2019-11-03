using System;
using System.Collections.Generic;

namespace SIGEM_BIDSS.Models
{
    public partial class TbEstado
    {
        public int EstId { get; set; }
        public string EstDescripcion { get; set; }
        public int EstUsuarioCrea { get; set; }
        public DateTime EstFechaCrea { get; set; }
        public int? EstUsuarioModifica { get; set; }
        public DateTime? EstFechaModifica { get; set; }
    }
}
