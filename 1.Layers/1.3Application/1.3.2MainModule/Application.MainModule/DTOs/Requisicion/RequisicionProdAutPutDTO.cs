using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Requisicion
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

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadRequerida")]
        public decimal CantidadRequerida { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "CantidadAComprar")]
        public decimal CantidadAComprar { get; set; }
    }
}
