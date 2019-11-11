using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(EmpleadosMetaData))]
    public partial class tbEmpleado
    {
        [NotMapped]
        public List<tbEmpleado> EmpleadoList { get; set; }
        [NotMapped]
        public string ge_Id { get; set; }
        [NotMapped]
        public string ge_Description { get; set; }
    }


    public class EmpleadosMetaData
    {
        [Display(Name = "ID EMPLEADO")]
        public short emp_Id { get; set; }

        [Display(Name = "NOMBRES")]
        public string emp_Nombres { get; set; }

        [Display(Name = "APELLIDOS")]
        public string emp_Apellidos { get; set; }

        [Display(Name = "SEXO")]
        public string emp_Sexo { get; set; }

        [Display(Name = "FECHA NACIMIENTO")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public string emp_FechaNacimiento { get; set; }

        [Display(Name = "IDENTIFICACIÓN")]
        public string emp_Identificacion { get; set; }

        [Display(Name = "TELEFÓNO")]
        public string emp_Telefono { get; set; }

        [Display(Name = "CORREO ELECTRONICO")]
        public string emp_CorreoElectronico { get; set; }

        [Display(Name = "TIPO SANGRE")]
        public int tps_Id { get; set; }

        [Display(Name = "PUESTO")]
        public int pto_Id { get; set; }

        [Display(Name = "FECHA INGRESÓ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public string emp_FechaIngreso { get; set; }

        [Display(Name = "DIRECCIÓN")]
        public string emp_Direccion { get; set; }

        [Display(Name = "FECHA INACTIVACIÓN")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public string emp_RazonInactivacion { get; set; }

        [Display(Name = "ESTADO")]
        public int est_Id { get; set; }

        [Display(Name = "IMAGEN")]
        public string emp_PathImage { get; set; }

        [Display(Name = "MUNICIPIO")]
        public string mun_Id { get; set; }

        [Display(Name = "USUARIO CREA")]
        public Nullable<int> emp_UsuarioCrea { get; set; }

        [Display(Name = "FECHA CREA")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> emp_FechaCrea { get; set; }

        [Display(Name = "USUARIO MODIFICA")]
        public Nullable<int> emp_UsuarioModifica { get; set; }

        [Display(Name = "FECHA MODIFICA")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }

        public virtual tbMunicipio tbMunicipio { get; set; }
        public virtual tbPuesto tbPuesto { get; set; }
        public virtual tbTipoSangre tbTipoSangre { get; set; }
    }



}