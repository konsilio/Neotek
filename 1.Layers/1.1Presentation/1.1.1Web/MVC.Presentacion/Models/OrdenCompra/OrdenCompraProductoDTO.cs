using Exceptions.MainModule.Validaciones;
using MVC.Presentacion.Models.Requisicion;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Presentacion.Models.OrdenCompra
{
    [Serializable]
    public class OrdenCompraProductoDTO 
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProducto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Producto")]
        public string Producto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdTipoProducto")]
        public int IdTipoProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "TipoProducto")]
        public string TipoProducto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCentroCosto")]
        public int IdCentroCosto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CentroCosto")]
        public string CentroCosto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdUnidad")]
        public int IdUnidad { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Unidad")]
        public string Unidad { get; set; }     

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Aplicacion")]
        public string Aplicacion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadAComprar")]
        public decimal CantidadAComprar { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProdveedor")]
        public int IdProdveedor { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IDCuentaContable")]
        public int IdCuentaContable { get; set; }

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