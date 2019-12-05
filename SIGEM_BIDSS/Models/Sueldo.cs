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
   
      
        public int sue_Id { get; set; }

        [Display(Name = "Nombre Empleado")]
       
        public int emp_Id { get; set; }


        [Display(Name = "Nombre Moneda")]
       
        public short tmo_Id { get; set; }


        [Display(Name = "Cantidad")]
        
        public Nullable<decimal> sue_Cantidad { get; set; }


        [Display(Name = "Sueldo Anterior")]
       
        public Nullable<int> sue_SueldoAnterior { get; set; }


        [Display(Name = "Estado")]
        
        public bool sue_Estado { get; set; }


        [Display(Name = "Razón Inacticación")]
       
        public string sue_RazonInactivo { get; set; }

        //public int sue_UsuarioCrea { get; set; }

        //public Nullable<System.DateTime> sue_FechaCrea { get; set; }

        //public Nullable<int> ue_UsuarioModifica { get; set; }

        //public Nullable<System.DateTime> sue_FechaModifica { get; set; }
    }
}