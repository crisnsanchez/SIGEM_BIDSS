using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
      
    [MetadataType(typeof(SueldoMetaData))]


    public partial class tbSueldo
    {
      
    }
    public class SueldoMetaData
    {

        [Display(Name = "ID")]
        public int sue_Id { get; set; }

        [Display(Name = "Nombre Empleado")]
        public int emp_Id { get; set; }

        [Display(Name = "Cantidad")]
        public Nullable<decimal> sue_Cantidad { get; set; }


        [Display(Name = "Tipo Moneda")]
        public short tmo_Id { get; set; }

        [Display(Name = "Usuario Crea")]
        public int sue_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public Nullable<System.DateTime> sue_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> sue_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> sue_FechaModifica { get; set; }

        public virtual tbMoneda tbMoneda { get; set; }
        public virtual tbEmpleado tbEmpleado { get; set; }
    }
}