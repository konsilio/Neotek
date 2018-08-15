using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
    [Serializable]
    public class ProductoOCDTO : Requisicion.RequisicionProductoAutorizacionDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdCentroCosto")]
        public int IdCentroCosto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CentroCosto")]
        public string CentroCosto { get; set; }

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
