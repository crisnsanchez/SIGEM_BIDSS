using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(EmpleadoMetaData))]
    public partial class tbEmpleado
    {
        [NotMapped]
        public List<tbEmpleado> EmpleadoList { get; set; }
    }

    public class EmpleadoMetaData
    {
        [Display(Name = "ID")]
        public short emp_Id { get; set; }
        [Display(Name = "Nombres")]
        public string emp_Nombres { get; set; }
        [Display(Name = "Apellidos")]
        public string emp_Apellidos { get; set; }
        [Display(Name = "Sexo")]
        public string emp_Sexo { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        public string emp_FechaNacimiento { get; set; }
        [Display(Name = "Identificación")]
        public string emp_Identificacion { get; set; }
        [Display(Name = "Teléfono")]
        public string emp_Telefono { get; set; }
        [Display(Name = "Correo Electronico")]
        public string emp_CorreoElectronico { get; set; }
        [Display(Name = "Tipo Sangre")]
        public int tps_Id { get; set; }
        [Display(Name = "Puesto")]
        public int pto_Id { get; set; }
        [Display(Name = "Fecha de Ingreso")]
        public string emp_FechaIngreso { get; set; }
        [Display(Name = "Dirección")]
        public string emp_Direccion { get; set; }
        [Display(Name = "Razon Inactivo")]
        public string emp_RazonInactivacion { get; set; }
        [Display(Name = "Estado")]
        public string est_Id { get; set; }
        [Display(Name = "Imagen")]
        public string emp_PathImage { get; set; }
        [Display(Name = "Municipio")]
        public string mun_Id { get; set; }
        [Display(Name = "Usuario Crea")]
        public Nullable<int> emp_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public Nullable<System.DateTime> emp_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> emp_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }

      
    }


}