using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionProductoAutorizacionDTO : RequisicionProductoNuevoDTO
    {        
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadAlmacenActual")]
        public decimal CantidadAlmacenActual { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadAComprar")]
        public decimal CantidadAComprar { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "AutorizaEntrega")]
        public bool AutorizaEntrega { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "AutorizaCompra")]
        public bool AutorizaCompra { get; set; }
        public bool EsActivoVenta { get; set; }
        public bool EsGas { get; set; }
        public bool EsTransporteGas { get; set; }
    }
}