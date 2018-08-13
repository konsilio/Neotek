using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    [Serializable]
    public class RequisicionProdAutPutDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProducto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "AutorizaEntrega")]
        public bool AutorizaEntrega { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "AutorizaCompra")]
        public bool AutorizaCompra { get; set; }
    }
}