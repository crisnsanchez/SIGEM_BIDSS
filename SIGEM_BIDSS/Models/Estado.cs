using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(EstadoaMetaData))]
    public partial class tbEstado
    {
        [NotMapped]
        public List<tbEstado> EstadoList { get; set; }
    }

    public class EstadoaMetaData
    {
        [Display(Name = "ID")]
        public int est_Id { get; set; }
        [Display(Name = "Estado")]
        public string est_Descripcion { get; set; }
        [Display(Name = "Usuario Crea")]
        public int est_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime est_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> est_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> est_FechaModifica { get; set; }
    }
}