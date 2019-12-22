﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    [MetadataType(typeof(ProveedorMetaData))]
    public partial class tbProveedor
    {
        [NotMapped]
        public List<tbProveedor> ProveedorList { get; set; }

   
    }

    public class ProveedorMetaData
    {
        [Display(Name = "ID")]
        public int prov_Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        public string prov_Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Contacto")]
        public string prov_NombreContacto { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Dirección")]
        public string prov_Direccion { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Municipio")]
        public string mun_codigo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]

        [Display(Name = "Correo Electrónico")]
        public string prov_Email { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Phone( ErrorMessage = "Por favor complete el campo teléfono")]
        //[MinLength(11, ErrorMessage = "El campo Teléfono debe tener minimo {1} caracteres")]
        public string prov_Telefono { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "RTN")]
        public string prov_RTN { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Actividad Económica")]
        public short acte_Id { get; set; }
  

    }
}