using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models

{

    [ModelMetadataType(typeof(TbDepartamentoMetadata))]
    public partial class TbDepartamento
    {


    }
    public class TbDepartamentoMetadata
    {
        [Display(Name = "Código Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string DepId { get; set; }

        [Display(Name = "Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string DepDescripcion { get; set; }


        //public int DepUsuarioCrea { get; set; }


        //public DateTime DepFechaCrea { get; set; }


        //public int? DepUsuarioModifica { get; set; }



        //public DateTime? DepFechaModifica { get; set; }




    }
}
