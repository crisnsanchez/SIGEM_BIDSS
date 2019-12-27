﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(CategoriaMetaData))]
    public partial class tbProductoCategoria
    {
       
    }

    public class CategoriaMetaData
    {
        [Display(Name = "ID")]
        public int pcat_Id { get; set; }

        [Display(Name = "Descripción")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MinLength(5, ErrorMessage = "Máximo {1} caracteres")]
        public string pcat_Descripcion { get; set; }

    }
}