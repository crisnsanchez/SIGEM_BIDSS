using System;
using System.Collections.Generic;

namespace SIGEM_BIDSS.Models
{
    public partial class TbMunicipio
    {
        public TbMunicipio()
        {
            TbEmpleado = new HashSet<TbEmpleado>();
        }

        public string MunId { get; set; }
        public string DepId { get; set; }
        public string MunNombre { get; set; }
        public int MunUsuarioCrea { get; set; }
        public DateTime MunFechaCrea { get; set; }
        public int? MunUsuarioModifica { get; set; }
        public DateTime? MunFechaModifica { get; set; }

        public virtual TbDepartamento Dep { get; set; }
        public virtual ICollection<TbEmpleado> TbEmpleado { get; set; }
    }
}
