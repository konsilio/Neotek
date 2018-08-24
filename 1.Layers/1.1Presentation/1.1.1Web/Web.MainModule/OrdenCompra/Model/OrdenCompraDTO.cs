using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.MainModule.OrdenCompra.Model
{
    public class OrdenCompraDTO
    {        
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdOrdenCompra")]
        public int IdOrdenCompra { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdOrdenCompraEstatus")]
        public byte IdOrdenCompraEstatus { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdRequisicion")]
        public int IdRequisicion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProveedor")]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCentroCosto")]
        public int IdCentroCosto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCuentaContable")]
        public int IdCuentaContable { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "NumOrdenCompra")]
        public string NumOrdenCompra { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "EsActivoVenta")]
        public bool EsActivoVenta { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "EsGas")]
        public bool EsGas { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "SubtotalSinIva")]
        public decimal SubtotalSinIva { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "SubtotalSinIeps")]
        public decimal SubtotalSinIeps { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Iva")]
        public decimal Iva { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Ieps")]
        public decimal Ieps { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "EsTransporteGas")]
        public bool EsTransporteGas { get; set; }        
    }
}