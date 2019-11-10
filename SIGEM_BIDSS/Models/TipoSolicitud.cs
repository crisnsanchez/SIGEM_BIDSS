using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{

    [MetadataType(typeof(TipoSolicitudMetaData))]
    public partial class tbTipoSolicitud
    {
        [NotMapped]
        public List<tbTipoMoneda> TipoSolicitudList { get; set; }
    }

    public class TipoSolicitudMetaData
    {
        [Display(Name = "ID")]
        public int tipsol_Id { get; set; }
        [Display(Name = "Tipo Solicitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "Máximo {1} caracteres")]
        [MinLength(3, ErrorMessage = "Minimo {1} caracteres")]
        public string tipsol_Descripcion { get; set; }
        [Display(Name = "Usuario Crea")]
        public int tipsol_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime tipsol_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tipsol_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tipsol_FechaModifica { get; set; }
    }
}