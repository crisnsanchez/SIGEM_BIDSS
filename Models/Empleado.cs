using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIGEM_BIDSS.Models
{
    public partial class TbEmpleado
    {
        [NotMapped]
        public string DepId { get; set; }
        public short EmpId { get; set; }
        public string EmpNombres { get; set; }
        public string EmpApellidos { get; set; }
        public string EmpSexo { get; set; }
        public string EmpFechaNacimiento { get; set; }
        public string EmpIdentificacion { get; set; }
        public string EmpTelefono { get; set; }
        public string EmpCorreoElectronico { get; set; }
        public int TpsId { get; set; }
        public int PtoId { get; set; }
        public string EmpFechaIngreso { get; set; }
        public string EmpDireccion { get; set; }
        public string EmpRazonInactivacion { get; set; }
        public string EmpEstado { get; set; }
        public string EmpPathImage { get; set; }
        public string MunId { get; set; }
        public int EmpUsuarioCrea { get; set; }
        public DateTime EmpFechaCrea { get; set; }
        public int? EmpUsuarioModifica { get; set; }
        public DateTime? EmpFechaModifica { get; set; }

        public virtual TbMunicipio Mun { get; set; }
        public virtual TbPuesto Pto { get; set; }
        public virtual TbTipoSangre Tps { get; set; }
    }
}
