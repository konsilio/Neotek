using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionProdAutPutDTO : RequisicionProductoDTO
    {   
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadRequerida")]
        public decimal CantidadRequerida { get; set; }

    }
}