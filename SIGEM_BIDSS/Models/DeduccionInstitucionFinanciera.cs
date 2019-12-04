using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(DeduccionInstitucionFinancieraMetadata))]
  
    public partial class tbDeduccionInstitucionFinanciera
    {
       
    }


    public class DeduccionInstitucionFinancieraMetadata
    {


        [Display(Name = "Id Deducción")]
        public int deif_IdDeduccionInstFinanciera { get; set; }

        [Display(Name = "Id Empleado")]
        public short emp_Id { get; set; }

        [Display(Name = "Id Institución Financiera")]
        public int insf_IdInstitucionFinanciera { get; set; }

        [Display(Name = "Monto Deducción")]
        public Nullable<decimal> deif_Monto { get; set; }

        [Display(Name = "Comentarios")]
        public string deif_Comentarios { get; set; }


        [Display(Name = "Usuario Crea")]
        public int deif_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime deif_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> deif_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> deif_FechaModifica { get; set; }

        [Display(Name = "Deducción Activo")]
        public bool deif_Activo { get; set; }
        public Nullable<bool> deif_Pagado { get; set; }

        public virtual tbEmpleado tbEmpleado { get; set; }
        public virtual tbInstitucionesFinancieras tbInstitucionesFinancieras { get; set; }
    }
}