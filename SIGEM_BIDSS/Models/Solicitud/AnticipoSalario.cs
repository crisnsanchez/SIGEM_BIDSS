using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(AnticipoSalarioMetadata))]
    public partial class tbAnticipoSalario
    {


    }
    public class AnticipoSalarioMetadata
    {
        [Display(Name = "ID")]
        public int Ansal_Id { get; set; }

        [Display(Name = "Código")]
        public string Ansal_Correlativo { get; set; }

        [Display(Name = "Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Jefe Inmediato")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")] 
        public int Ansal_JefeInmediato { get; set; }

        [Display(Name = "Fecha de Solicitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public System.DateTime Ansal_GralFechaSolicitud { get; set; }

        [Display(Name = "Monto a Solicitar")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")] 
        public decimal Ansal_MontoSolicitado { get; set; }

        [Display(Name = "Tipo de Salario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")] 
        public int tpsal_id { get; set; }

        [Display(Name = "Justificacion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string Ansal_Justificacion { get; set; }

        [Display(Name = "Comentario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")] 
        public string Ansal_Comentario { get; set; }

        [Display(Name = "Estado")]
        public int est_Id { get; set; }

        [Display(Name = "Razon de Rechazo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string Ansal_RazonRechazo { get; set; }

        public int Ansal_UsuarioCrea { get; set; }
        public System.DateTime Ansal_FechaCrea { get; set; }
        public Nullable<int> Ansal_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> Ansal_FechaModifica { get; set; }
    }

    
}