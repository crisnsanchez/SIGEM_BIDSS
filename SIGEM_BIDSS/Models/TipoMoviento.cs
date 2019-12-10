using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{

    [MetadataType(typeof(TipoMovientoMetaData))]
    public partial class tbTipoMovimiento
    {
  
    }

    public class TipoMovientoMetaData
    {

        [Display(Name = "ID")]
        public int tipmo_id { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public string tipmo_Movimiento { get; set; }

        [Display(Name = "Usuario Crea")]
        public int tipmo_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime tipmo_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tipmo_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tipmo_FechaModifica { get; set; }
    }
}