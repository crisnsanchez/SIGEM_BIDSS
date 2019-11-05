using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models
{
    [ModelMetadataType(typeof(TbPuestometada))]
    public partial class TbPuesto
    {


    }

    public class TbPuestometada
    {
        [Display(Name = "ID")]
        public int PtoId { get; set; }

        [Display(Name = "ID Puesto")]
        public int AreId { get; set; }

        [Display(Name = "Descripcion")]
        public string PtoDescripcion { get; set; }
    }
}
