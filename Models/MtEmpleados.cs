using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models
{
    [ModelMetadataType(typeof(TbEmpleadometadata))]
    public partial class TbEmpleado
    {


    }
    public class TbEmpleadometadata
    {
        [Display(Name = "ID")]
        public short EmpId { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "No se permiten mas de 50 Caracteres.")]
        public string EmpNombres { get; set; }

        [Display(Name = "Apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "No se permiten mas de 50 Caracteres.")]
        public string EmpApellidos { get; set; }

        [Display(Name = "Sexo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string EmpSexo { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string EmpFechaNacimiento { get; set; }

        [Display(Name = "Número de Identidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
    
        public string EmpIdentificacion { get; set; }

        [Display(Name = "Telefóno")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "No se permiten mas de 50 Caracteres.")]
        public string EmpTelefono { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]

        public string EmpCorreoElectronico { get; set; }

        [Display(Name = "Tipo de Sangre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int TpsId { get; set; }

        [Display(Name = "Puesto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public int PtoId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha Ingreso")]
        public string EmpFechaIngreso { get; set; }

        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "No se permiten mas de 50 Caracteres.")]
        public string EmpDireccion { get; set; }

        [Display(Name = "Razón de Inactivación")]
        public string EmpRazonInactivacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Estado")]
        public string EmpEstado { get; set; }

        [Display(Name = "Imagen")]
        public string EmpPathImage { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Código Municipio")]
        public string MunId { get; set; }

        [Display(Name = "Creado por ")]
        public int EmpUsuarioCrea { get; set; }

        [Display(Name = "Creado el")]
        public DateTime EmpFechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public int? EmpUsuarioModifica { get; set; }

        [Display(Name = "Modificado el")]
        public DateTime? EmpFechaModifica { get; set; }
    }
}
