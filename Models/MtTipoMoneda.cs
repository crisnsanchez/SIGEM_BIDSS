using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SIGEM_BIDSS.Models
{
    [ModelMetadataType(typeof(TipoMonedametada))]
    public partial class TipoMoneda
    {


    }
    public class TipoMonedametada
    {
        [Display(Name = "ID")]
        public short TmoId { get; set; }

        [Display(Name = "Abreviatura")]
        public string TmoAbreviatura { get; set; }

        [Display(Name = "Nombre")]
        public string TmoNombre { get; set; }

        //public int TmoUsuarioCrea { get; set; }
        //public DateTime TmoFechaCrea { get; set; }
        //public int? TmoUsuarioModifica { get; set; }
        //public DateTime? TmoFechaModifica { get; set; }
    }
}
