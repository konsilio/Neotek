using Exceptions.MainModule.Validaciones;
using MVC.Presentacion.Models.Requisicion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    [Serializable]
    public class ProductoOCDTO : RequisicionProductoAutorizacionDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProdveedor")]
        public int IdProdveedor { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IDCuentaContable")]
        public int IdCuentaContable { get; set; }

        public string CuentaContable { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Descuento")]
        public decimal Descuento { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IVA")]
        public decimal IVA { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IEPS")]
        public decimal IEPS { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Importe")]
        public decimal Importe { get; set; }
    }
}