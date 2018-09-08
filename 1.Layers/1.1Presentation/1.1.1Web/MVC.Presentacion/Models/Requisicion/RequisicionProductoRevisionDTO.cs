using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionProductoRevisionDTO : RequisicionProductoNuevoDTO
    {     
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "RevisionFisica")]
        public bool RevisionFisica { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadAlmacenActual")]
        public decimal CantidadAlmacenActual { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadAComprar")]
        public decimal CantidadAComprar { get; set; }
    }
}